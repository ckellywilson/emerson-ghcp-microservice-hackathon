# Student Service Challenge (Day 2)

## Challenge statement

Extract the **Student aggregate** into a standalone microservice while preserving behavior and establishing sovereign data ownership.

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

## Success criteria

- Student bounded context and aggregate boundaries are documented.
- Team presents an ownership decision for `Enrollment` with trade-offs.
- A sovereign data model for Student service is defined.
- Event contracts are documented for Student ↔ Course collaboration.
- Test strategy covers contract, integration, and idempotency checks.

## Scoring rubric (100 points)

| Category | Points | What earns full credit |
|---|---:|---|
| DDD boundary quality | 25 | Clear bounded context, aggregate root, invariants, and ubiquitous language |
| Data ownership design | 25 | Coherent ownership for `Enrollment`, no shared tables, justified consistency model |
| Integration architecture | 20 | Event-first integration, explicit contracts, ACL boundaries |
| Implementation viability | 15 | Incremental strangler plan with rollback and observability checkpoints |
| Quality strategy | 15 | Practical test and review plan tied to service contracts |
