# Notification Extraction Facilitator Notes (Day 1)

Audience: instructors/facilitators running the Day 1 showcase.

Student-facing workflow remains in:
- `showcase/docs/notification-extraction-walkthrough.md`

Use this companion file for context, coaching prompts, and quality checks during non-code steps.

## Facilitation mode guidance

- Run scripted slash-commands in Copilot Chat Agent mode.
- Use regular Chat mode between steps for fast clarification and reframing.
- Reinforce that planning/contract outputs are valid deliverables even when no files are generated.
- Keep teams on thin vertical slices; do not allow scope expansion.

## Step 1 - Plan

Purpose:
- Lock extraction scope, seams, and rollback points before implementation.

What to say:
- "Success in this step is a decision package, not code output."

A good output includes:
- Notification bounded context and explicit non-goals.
- Strangler phases with rollback points.
- Initial API/event contract direction.
- A realistic thin vertical slice target for the showcase.

Red flags:
- Big-bang migration language.
- No rollback path.
- Scope creep into unrelated domains.

Timebox:
- 10 to 15 minutes.

## Step 2 - Define bounded context

Purpose:
- Align language and invariants so implementation does not drift.

What to say:
- "We are validating boundaries and domain language before writing code."

A good output includes:
- Aggregate root and invariants.
- Ubiquitous language glossary.
- Upstream/downstream map and integration edges.

Red flags:
- Mixed terminology across monolith and service.
- Shared ownership implied for the same entity.

Timebox:
- 10 to 15 minutes.

## Step 3 - Design service API (contract-first)

Purpose:
- Make contracts explicit and stable before scaffolding.

What to say:
- "Contracts are the product; code is the implementation detail."

A good output includes:
- Endpoint request/response/error definitions.
- Versioned event contracts.
- Idempotency and retry expectations.
- ACL translation points for legacy callers.

Red flags:
- Internal domain model leakage across service boundary.
- Missing error model.
- Missing idempotency/retry behavior.

Timebox:
- 15 to 20 minutes.

## Step 4 - Scaffold service

Purpose:
- Generate a minimal runnable shell that follows approved contracts.

What to say:
- "Do not optimize structure yet; verify the shell compiles and boundaries are clean."

A good output includes:
- Service project structure outside monolith.
- Stable contract interfaces.
- Boundary adapter placement at monolith edge.
- Local configuration and successful build.

Red flags:
- Direct coupling back into monolith internals.
- Contract drift from Step 3 outputs.

Timebox:
- 30 to 45 minutes.

## Step 5 - Wire event seam and ACL

Purpose:
- Establish explicit integration behavior with idempotent handling.

What to say:
- "Use existing Service Bus seam; preserve producer stability."

A good output includes:
- Explicit event contracts and versioning statement.
- Idempotent inbound handler strategy.
- ACL mappings for legacy payload translation.

Red flags:
- Shared-table or direct model coupling.
- No idempotency strategy.

Timebox:
- 30 to 45 minutes.

## Step 6 - Sovereign datastore carve-out

Purpose:
- Confirm ownership boundaries and migration posture.

What to say:
- "For Notification on Day 1, simple is correct. This carve-out is intentionally light."

A good output includes:
- Explicit ownership statement.
- No shared-table coupling.
- Event-driven integration stance for cross-context data.

Red flags:
- Unnecessary migration complexity.
- Pulling Student-specific ownership rules into Notification walkthrough.

Timebox:
- 5 to 10 minutes.

## Step 7 - Test strategy

Purpose:
- Define evidence needed for confidence and demo readiness.

What to say:
- "We are validating behavior and integration safety, not only line coverage."

A good output includes:
- Contract tests.
- Event idempotency/retry tests.
- Integration tests for seam and adapters.
- Fresh-clone build as smoke gate.

Red flags:
- Only unit tests with no seam coverage.
- No failure or retry scenario checks.

Timebox:
- 15 to 20 minutes.

## Step 8 - Review and readiness decision

Purpose:
- Make an explicit go/no-go decision for the current slice.

What to say:
- "Decide based on principles and ADR alignment, not optimism."

A good output includes:
- Principles/ADR conformance check.
- Risks and follow-up slice recommendation.
- Clear approve-or-iterate decision.

Red flags:
- No explicit decision.
- Decision made without evidence.

Timebox:
- 10 minutes.

## Fast coaching loop after each non-code step

Use this 4-question loop:
1. What did we decide?
2. What evidence supports that decision?
3. What remains risky?
4. Does that risk block today's thin-slice demo?

## Evidence to capture for presentations

Collect lightweight artifacts during the session:
- Slash-command invocations used.
- Key chat outputs for boundary/contract decisions.
- One architecture sketch of seam and ownership.
- Build/test proof for implemented slice.
- Trade-offs and next-slice recommendation.
