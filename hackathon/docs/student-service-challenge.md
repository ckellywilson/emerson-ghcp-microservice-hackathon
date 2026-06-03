# Student Service Challenge (Day 2)

## Challenge statement

Extract the **Student aggregate** into a standalone microservice while preserving behavior and establishing sovereign data ownership.
Aggregate boundaries are already decided in [`../../docs/architecture/adr/0003-student-service-data-ownership.md`](../../docs/architecture/adr/0003-student-service-data-ownership.md) (**Accepted**) and are a workshop requirement.

Teams are **not** expected to deliver a full production-ready service in one day. The expected outcome is a Copilot-driven execution package plus one thin, demonstrable vertical slice.

## Core complication: Enrollment coupling

`Enrollment` links Student and Course, and the seam must be implemented (not re-debated):

1. Treat `Student` as aggregate root and `Enrollment` as its child entity.
2. Sever `Enrollment.Course` object navigation and use `CourseID` identity reference + read projection.
3. Keep Course in a separate bounded context; collaborate via events + ACL with no shared tables.

Use guidance from [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) and [`../../docs/architecture/adr/0003-student-service-data-ownership.md`](../../docs/architecture/adr/0003-student-service-data-ownership.md).

## Required GHCP assets

- Skills:
  - [`../../.github/skills/ddd-bounded-contexts/SKILL.md`](../../.github/skills/ddd-bounded-contexts/SKILL.md)
  - [`../../.github/skills/microservice-decomposition/SKILL.md`](../../.github/skills/microservice-decomposition/SKILL.md)
  - [`../../.github/skills/sovereign-data-stores/SKILL.md`](../../.github/skills/sovereign-data-stores/SKILL.md)
  - [`../../.github/skills/event-driven-integration/SKILL.md`](../../.github/skills/event-driven-integration/SKILL.md)
- Prompts:
  - [`../../.github/prompts/define-bounded-context.prompt.md`](../../.github/prompts/define-bounded-context.prompt.md)
  - [`../../.github/prompts/design-service-api.prompt.md`](../../.github/prompts/design-service-api.prompt.md)
  - [`../../.github/prompts/scaffold-service.prompt.md`](../../.github/prompts/scaffold-service.prompt.md)
  - [`../../.github/prompts/wire-event-seam.prompt.md`](../../.github/prompts/wire-event-seam.prompt.md)
  - [`../../.github/prompts/plan-data-migration.prompt.md`](../../.github/prompts/plan-data-migration.prompt.md)
  - [`../../.github/prompts/test-strategy.prompt.md`](../../.github/prompts/test-strategy.prompt.md)
  - [`../../.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md)

## Constraints

- No shared database tables across services.
- Service contracts must be explicit and versionable.
- Cross-service interactions should prefer asynchronous events.
- Any sync call must have clear ownership and fallback behavior.
- ACLs are required when crossing legacy-monolith boundaries.

## Timebox & Milestones (single-day hackathon)

Suggested pace for ~4.5 hours of build time before presentations:

| Time (example) | Phase | Expected output |
|---|---|---|
| 09:00–09:20 | Kickoff and seam mapping (Copilot-assisted) | Agent-mode map of Student/Enrollment/Course coupling and extraction seams |
| 09:20–10:00 | Prompt-driven boundary + contract setup | Confirm ADR-0003 boundary, draft API/events/ACL contracts |
| 10:00–12:00 | Extraction + scaffolding implementation | StudentService scaffold and first end-to-end slice (identity ref + projection + event flow) |
| 13:00–14:10 | Test generation + hardening | Copilot-generated tests and verification for contract/integration/idempotency |
| 14:10–14:30 | Demo prep and evidence pack | Working vertical slice demo, prompt/skill transcript, trade-offs + next slice |

Checkpoint expectations:
- **Checkpoint 1 (09:20):** coupling map captured; ADR-0003 boundary understood.
- **Checkpoint 2 (10:00):** service/API/event/ACL plan agreed.
- **Checkpoint 3 (12:00):** vertical slice path implemented and runnable.
- **Checkpoint 4 (14:10):** test evidence captured for implemented seam.

## Success criteria

- Team implements the prescribed ADR-0003 boundary (Student root, Enrollment child, Course by identity).
- Team demonstrates Copilot-driven execution flow (agent mode + prompt assets + skills) with reproducible invocations.
- A sovereign data model for Student service is defined.
- Event contracts + ACL handling are documented for Student ↔ Course collaboration.
- At least one thin vertical extraction slice is implemented and demonstrated.
- Test strategy (with evidence for the slice) covers contract, integration, and idempotency checks.
- Final proposal identifies next strangler slices and rollback points.

## Scoring rubric (100 points)

| Category | Points | What earns full credit |
|---|---:|---|
| Effective GHCP usage | 30 | Strong agent-mode workflow, broad prompt/skill usage, and copy-pastable invocations tied to outcomes |
| Demonstrable vertical slice | 30 | Thin end-to-end slice works and reflects the prescribed Student/Enrollment/Course boundary |
| Integration and seam handling | 20 | `CourseID` identity reference, projection strategy, event contracts, and ACL boundaries are explicit |
| Quality strategy and tests | 15 | Practical test evidence and verification plan for contract/integration/idempotency |
| Insight/stretch | 5 | Clear reflection on trade-offs, including optional case for promoting Enrollment to its own aggregate later |
