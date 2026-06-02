# Microservice Principles (Canonical Reference)

This is the canonical architecture reference for all workshop guidance assets.

## 1) Domain-Driven Design (DDD)

- Define explicit **bounded contexts** with minimal overlap.
- Model **aggregates** around transactional consistency boundaries.
- Protect aggregate invariants through aggregate roots.
- Keep a shared **ubiquitous language** for domain terms and contracts.

See also:
- [`.github/skills/ddd-bounded-contexts/SKILL.md`](../../.github/skills/ddd-bounded-contexts/SKILL.md)
- [`target-architecture.md`](target-architecture.md)

## 2) Loose Coupling, High Cohesion

- A service owns a cohesive capability, not scattered CRUD slices.
- Dependencies across services should occur through contracts, not direct data access.
- Avoid shared internal models; expose only stable APIs/events.

See also:
- [`.github/prompts/design-service-api.prompt.md`](../../.github/prompts/design-service-api.prompt.md)

## 3) Sovereign Data Stores (Database per service)

- Each service owns its data schema and persistence lifecycle.
- No shared tables across bounded contexts.
- Cross-service reads use APIs, events, or projections.
- For migration, prefer incremental replication/projection and outbox patterns.

See also:
- [`.github/skills/sovereign-data-stores/SKILL.md`](../../.github/skills/sovereign-data-stores/SKILL.md)
- [`adr/0003-student-service-data-ownership.md`](adr/0003-student-service-data-ownership.md)

## 4) Event-Driven Communication

- Use domain events for cross-context collaboration.
- Version message contracts and preserve backward compatibility.
- Implement idempotent consumers and retry-safe handlers.
- Route legacy integrations through anti-corruption layers.

See also:
- [`.github/skills/event-driven-integration/SKILL.md`](../../.github/skills/event-driven-integration/SKILL.md)
- [`adr/0002-notification-service-boundary.md`](adr/0002-notification-service-boundary.md)

## 5) Anti-Corruption Layers (ACL)

- Keep translation logic at boundaries between monolith and extracted services.
- Prevent legacy model leakage into new service domain models.
- Treat ACL code as explicit, testable mapping logic.

## 6) Strangler-Fig Migration Strategy

- Extract the cleanest seam first (Notification in this workshop).
- Replace behavior in thin slices, not big-bang rewrites.
- Maintain rollback points and measurable acceptance gates per slice.

See also:
- [`adr/0001-strangler-fig-extraction.md`](adr/0001-strangler-fig-extraction.md)
- [`.github/skills/microservice-decomposition/SKILL.md`](../../.github/skills/microservice-decomposition/SKILL.md)
