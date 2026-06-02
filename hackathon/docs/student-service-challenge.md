# Student Service Challenge (Day 2)

## Challenge statement

Extract the **Student aggregate** into a standalone microservice while preserving behavior and establishing sovereign data ownership.

Teams are **not** expected to deliver a full production-ready service in one day. The expected outcome is a high-quality architecture/design package plus one thin, demonstrable vertical slice.

## Core complication: Enrollment coupling

`Enrollment` links Student and Course. Each team must decide and defend:

1. Which service owns enrollment records and lifecycle transitions.
2. Which cross-service data is copied, queried, or projected.
3. How eventual consistency is enforced without shared tables.

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
  - [`../../.github/prompts/plan-data-migration.prompt.md`](../../.github/prompts/plan-data-migration.prompt.md)
  - [`../../.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md)

## Constraints

- No shared database tables across services.
- Service contracts must be explicit and versionable.
- Cross-service interactions should prefer asynchronous events.
- Any sync call must have clear ownership and fallback behavior.
- ACLs are required when crossing legacy-monolith boundaries.

## Timebox & Milestones (single-day hackathon)

Suggested pace for a ~6.5 hour working day:

| Time (example) | Phase | Expected output |
|---|---|---|
| 09:00–09:30 | Kickoff and context alignment | Team picks extraction scope and confirms assumptions |
| 09:30–10:45 | Bounded-context and data-ownership design | Context boundary, aggregate/invariants, `Enrollment` ownership decision |
| 10:45–11:45 | API and event contract design | Draft service API + versioned event contracts + ACL boundaries |
| 12:30–14:30 | Thin implementation slice | One end-to-end slice (API/action + persistence/event path) |
| 14:30–15:15 | Testing and verification | Contract/integration/idempotency checks for implemented slice |
| 15:15–16:00 | Final storyboard and demo prep | Architecture decisions, trade-offs, and next-slice rollout plan |

Checkpoint expectations:
- **Checkpoint 1 (10:45):** bounded context + ownership decision locked.
- **Checkpoint 2 (11:45):** contracts and rollout phases agreed.
- **Checkpoint 3 (14:30):** vertical slice demonstrable.
- **Checkpoint 4 (15:15):** test evidence captured.

## Success criteria

- Student bounded context and aggregate boundaries are documented.
- Team presents an ownership decision for `Enrollment` with trade-offs.
- A sovereign data model for Student service is defined.
- Event contracts are documented for Student ↔ Course collaboration.
- At least one thin vertical extraction slice is implemented and demonstrated.
- Test strategy (with evidence for the slice) covers contract, integration, and idempotency checks.
- Final proposal clearly identifies next strangler slices and rollback points.

## Scoring rubric (100 points)

| Category | Points | What earns full credit |
|---|---:|---|
| DDD boundary quality | 25 | Clear bounded context, aggregate root, invariants, and ubiquitous language |
| Data ownership design | 25 | Coherent ownership for `Enrollment`, no shared tables, justified consistency model |
| Integration architecture | 20 | Event-first integration, explicit contracts, ACL boundaries |
| Demonstrable vertical slice | 20 | A thin end-to-end slice works and aligns to the proposed architecture |
| Quality strategy | 10 | Practical test evidence and review plan tied to contracts/idempotency |
