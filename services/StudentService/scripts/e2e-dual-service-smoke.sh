#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../../.." && pwd)"
STUDENT_URL="http://localhost:5201"
MONOLITH_URL="http://localhost:5300"
STUDENT_LOG="/tmp/studentservice-e2e.log"
MONOLITH_LOG="/tmp/monolith-e2e.log"

cleanup() {
  if [[ -n "${STUDENT_PID:-}" ]] && kill -0 "$STUDENT_PID" 2>/dev/null; then
    kill "$STUDENT_PID" 2>/dev/null || true
  fi

  if [[ -n "${MONOLITH_PID:-}" ]] && kill -0 "$MONOLITH_PID" 2>/dev/null; then
    kill "$MONOLITH_PID" 2>/dev/null || true
  fi
}
trap cleanup EXIT

wait_for_http() {
  local url="$1"
  local name="$2"
  local attempts=60

  for ((i=1; i<=attempts; i++)); do
    if curl -fsS "$url" >/dev/null 2>&1; then
      echo "$name is ready at $url"
      return 0
    fi
    sleep 1
  done

  echo "$name did not become ready in time: $url"
  return 1
}

wait_for_status_prefix() {
  local url="$1"
  local name="$2"
  local attempts=60

  for ((i=1; i<=attempts; i++)); do
    local code
    code="$(curl -s -o /dev/null -w "%{http_code}" "$url" || true)"
    if [[ "$code" == 2* || "$code" == 3* ]]; then
      echo "$name is responding at $url (HTTP $code)"
      return 0
    fi
    sleep 1
  done

  echo "$name did not return HTTP 2xx/3xx in time: $url"
  return 1
}

echo "Starting StudentService API on $STUDENT_URL"
(
  cd "$ROOT_DIR"
  dotnet run --project services/StudentService/src/StudentService.Api --urls "$STUDENT_URL" >"$STUDENT_LOG" 2>&1
) &
STUDENT_PID=$!

wait_for_http "$STUDENT_URL/health" "StudentService"

echo "Starting monolith Web on $MONOLITH_URL"
(
  cd "$ROOT_DIR"
  ASPNETCORE_ENVIRONMENT=Development dotnet run --project monolith/ContosoUniversity.Web --urls "$MONOLITH_URL" >"$MONOLITH_LOG" 2>&1
) &
MONOLITH_PID=$!

wait_for_status_prefix "$MONOLITH_URL" "Monolith"

echo "Running StudentService vertical-slice flow"
create_student_response="$(curl -fsS -X POST "$STUDENT_URL/api/v1/students" \
  -H 'Content-Type: application/json' \
  -d '{"lastName":"Dual","firstMidName":"Run","enrollmentDate":"2026-06-05"}')"

echo "Create student response: $create_student_response"
student_id="$(echo "$create_student_response" | sed -n 's/.*"studentId":\([0-9]*\).*/\1/p')"
if [[ -z "$student_id" ]]; then
  echo "Unable to parse studentId from create response"
  exit 1
fi

list_students_response="$(curl -fsS "$STUDENT_URL/api/v1/students")"
echo "List students response: $list_students_response"
if ! echo "$list_students_response" | grep -q "\"studentId\":$student_id"; then
  echo "Created studentId $student_id was not found in GET /api/v1/students response"
  exit 1
fi

create_enrollment_response="$(curl -fsS -X POST "$STUDENT_URL/api/v1/students/$student_id/enrollments" \
  -H 'Content-Type: application/json' \
  -d '{"courseId":42}')"

echo "Create enrollment response: $create_enrollment_response"
enrollment_id="$(echo "$create_enrollment_response" | sed -n 's/.*"enrollmentId":\([0-9]*\).*/\1/p')"
if [[ -z "$enrollment_id" ]]; then
  echo "Unable to parse enrollmentId from enrollment response"
  exit 1
fi

update_grade_response="$(curl -fsS -X PATCH "$STUDENT_URL/api/v1/students/$student_id/enrollments/$enrollment_id/grade" \
  -H 'Content-Type: application/json' \
  -d '{"grade":"A"}')"

echo "Update grade response: $update_grade_response"

final_student_response="$(curl -fsS "$STUDENT_URL/api/v1/students/$student_id")"
echo "Final student response: $final_student_response"

if ! echo "$final_student_response" | grep -q '"grade":"A"'; then
  echo "Expected grade A not found in final student response"
  exit 1
fi

echo "Verifying monolith still responds while StudentService flow runs"
monolith_status="$(curl -s -o /dev/null -w "%{http_code}" "$MONOLITH_URL" || true)"
if [[ "$monolith_status" != 2* && "$monolith_status" != 3* ]]; then
  echo "Monolith is not healthy during flow. HTTP status: $monolith_status"
  exit 1
fi

echo "E2E dual-service smoke test passed"
echo "StudentService log: $STUDENT_LOG"
echo "Monolith log: $MONOLITH_LOG"
