---
description: Wire the event seam with explicit contracts, idempotent handling, and anti-corruption adapters at the monolith boundary.
agent: "agent"
---

Wire the event seam between **{{source_system}}** and **{{service_name}}**.

Use these references:
- `.github/skills/event-driven-integration/SKILL.md`
- `docs/architecture/adr/0002-notification-service-boundary.md`
- `docs/architecture/adr/0003-student-service-data-ownership.md`
- `docs/architecture/microservice-principles.md`

Output:
1. Service Bus event flow and ownership boundaries.
2. Event contract definitions and versioning expectations.
3. Idempotent handler and retry expectations for inbound processing.
4. Anti-corruption adapter responsibilities at the monolith edge.
