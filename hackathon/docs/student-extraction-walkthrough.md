# Student Extraction Walkthrough (Day 2)

This team playbook demonstrates extraction of Student as the Day-2 microservice using GHCP guidance assets.

> Prompts in this repo are authored for Agent mode (`agent: "agent"` frontmatter). Use VS Code Copilot Chat in **Agent** mode, not Ask mode.

## Step-to-asset map

| Step | Goal | GHCP asset(s) used |
|---|---|---|
| 1 | Plan extraction scope and rollout | [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md), [`.github/skills/microservice-decomposition/SKILL.md`](../../.github/skills/microservice-decomposition/SKILL.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md), [`../../docs/architecture/adr/0001-strangler-fig-extraction.md`](../../docs/architecture/adr/0001-strangler-fig-extraction.md) |
| 2 | Confirm bounded context and aggregate boundaries | [`.github/skills/ddd-bounded-contexts/SKILL.md`](../../.github/skills/ddd-bounded-contexts/SKILL.md), [`.github/prompts/define-bounded-context.prompt.md`](../../.github/prompts/define-bounded-context.prompt.md), [`../../docs/architecture/adr/0003-student-service-data-ownership.md`](../../docs/architecture/adr/0003-student-service-data-ownership.md) |
| 3 | Design service API contracts first | [`.github/prompts/design-service-api.prompt.md`](../../.github/prompts/design-service-api.prompt.md), [`.github/instructions/microservice-extraction.instructions.md`](../../.github/instructions/microservice-extraction.instructions.md), [`../../docs/architecture/target-architecture.md`](../../docs/architecture/target-architecture.md) |
| 4 | Scaffold the service from contracts | [`.github/prompts/scaffold-service.prompt.md`](../../.github/prompts/scaffold-service.prompt.md), [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`.github/instructions/microservice-extraction.instructions.md`](../../.github/instructions/microservice-extraction.instructions.md) |
| 5 | Wire Student-to-Course event seam and ACL | [`.github/prompts/wire-event-seam.prompt.md`](../../.github/prompts/wire-event-seam.prompt.md), [`.github/skills/event-driven-integration/SKILL.md`](../../.github/skills/event-driven-integration/SKILL.md), [`../../docs/architecture/adr/0003-student-service-data-ownership.md`](../../docs/architecture/adr/0003-student-service-data-ownership.md), [`../../docs/architecture/target-architecture.md`](../../docs/architecture/target-architecture.md) |
| 6 | Plan sovereign datastore migration | [`.github/skills/sovereign-data-stores/SKILL.md`](../../.github/skills/sovereign-data-stores/SKILL.md), [`.github/prompts/plan-data-migration.prompt.md`](../../.github/prompts/plan-data-migration.prompt.md), [`../../docs/architecture/adr/0002-notification-service-boundary.md`](../../docs/architecture/adr/0002-notification-service-boundary.md), [`../../docs/architecture/adr/0003-student-service-data-ownership.md`](../../docs/architecture/adr/0003-student-service-data-ownership.md) |
| 7 | Test strategy and quality gates | [`.github/prompts/test-strategy.prompt.md`](../../.github/prompts/test-strategy.prompt.md), [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |

## Team script

### Step 1 — Plan
- Activate decomposition guidance.
- Run `/extract-microservice domain_name="Student" source_system="ContosoUniversity monolith" target_service="StudentService"`.
- Confirm strangler-fig increments and rollback points.
**Done when:**
  - Chat produced a written extraction plan covering boundary decisions from ADR-0003, strangler-fig phases with rollback points, API/event contracts, data migration, and a test/review checklist.
  - The `microservice-decomposition` skill activated and the plan treats Student as a leaf-first seam.
  - No files are created in this step — the output is the plan in chat. This is expected.

### Step 2 — Define bounded context
- Activate `ddd-bounded-contexts` skill.
- Run `/define-bounded-context domain_name="Student" system_name="ContosoUniversity"`.
- Implement ADR-0003 as written: Student is the aggregate root, Enrollment is owned inside StudentService (including `Grade`), and Course remains a separate aggregate referenced by identity.
**Done when:**
  - You have a bounded-context statement with scope/non-goals, aggregate invariants, and a context map aligned to ADR-0003.
  - The seam is explicit: sever `Enrollment.Course` object navigation, keep `CourseID` as the cross-context identity reference, and use a Course read projection for Student-side reads.
  - No files are created — the output is the context analysis in chat. Expected.

### Step 3 — Design API (contract-first)
- Run `/design-service-api service_name="StudentService"`.
- Define enrollment and grade workflows under StudentService ownership; treat Course details as projection/read-model data.
**Done when:**
  - You have REST/HTTP endpoint contracts covering requests, responses, errors, and versioned events for Student and Enrollment operations.
  - Contracts show that Course collaboration crosses a service boundary by identity/events (not direct object navigation).
  - No code files are created yet — the output is the contract design in chat. Expected.

### Step 4 — Scaffold service
- Run `/scaffold-service service_name="StudentService" source_system="ContosoUniversity monolith"`.
- Generate service shell from approved contracts.
- Keep monolith adapters at the boundary.
**Done when:**
  - A new `StudentService` project or solution exists outside `monolith/`, leaving the monolith unmodified.
  - The service contains Student + Enrollment domain types and boundary adapters consistent with ADR-0003 ownership.
  - Configuration wiring such as `Program.cs` and `appsettings.json` is present, and `dotnet build` for the new service succeeds.

### Step 5 — Wire event seam
- Run `/wire-event-seam service_name="StudentService" source_system="ContosoUniversity monolith"`.
- Use events + ACL at the boundary where StudentService collaborates with Course data.
- Keep ownership in StudentService for Enrollment lifecycle and `Grade`; treat Course as external.
**Done when:**
  - Event contracts and ACL translation cover Student/Enrollment changes and Course reference updates across the monolith boundary.
  - The implementation plan explicitly replaces `Enrollment.Course` object coupling with `CourseID` identity reference + Course read projection.
  - Inbound handlers define idempotency and retry behavior.

### Step 6 — Plan data migration
- Run `/plan-data-migration service_name="StudentService" source_data_model="ContosoUniversity EF Core model"`.
- Use ADR-0003 as the ownership decision and ADR-0002 as a reusable sovereign-data reference pattern (the prompt is domain-neutral by design).
- Ensure no shared-table coupling remains.
**Done when:**
  - Migration plan states StudentService owns Student + Enrollment data (including `Grade`) without shared tables.
  - Course references are handled by identity + projection, with event-driven synchronization and ACL boundaries.
  - The phased strangler rollout includes rollback points and consistency checks.

### Step 7 — Test
- Run `/test-strategy service_name="StudentService"`.
- Validate contract tests, event idempotency, and projection consistency.
**Done when:**
  - Contract tests, event-handler idempotency tests, and integration tests exist for the implemented Student/Enrollment slice.
  - Tests prove the seam behavior: Course coupling is via `CourseID` + projection/events, not direct aggregate navigation.
  - A fresh-clone `dotnet build` passes as the smoke test, matching the repo CI flow of `dotnet restore`, `dotnet build`, and `dotnet test` on PRs.
