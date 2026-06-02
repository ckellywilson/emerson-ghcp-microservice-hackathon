---
name: sovereign-data-stores
description: Auto-activate when designing service data ownership, database boundaries, migrations, and eventual-consistency patterns.
---

# Sovereign Data Stores Skill

Use this when defining data ownership and migrations.

## Heuristics
- One service owns one schema lifecycle; no shared write access.
- Never split ownership of a single table across services.
- Replace cross-service joins with APIs, projections, or materialized read models.
- Use outbox + event publication for reliable state propagation.
- Define stale-read tolerance and compensation paths.

## Enrollment guidance
- If `Enrollment` spans Student and Course concerns, pick one owner and publish events.
- Non-owners should consume events into local projections.

Reference: `docs/architecture/microservice-principles.md` and `docs/architecture/adr/0003-student-service-data-ownership.md`.
