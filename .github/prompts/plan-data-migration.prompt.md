---
description: Plan sovereign datastore carve-out, ownership rules, and eventual-consistency strategy for extracted services.
agent: "agent"
---

Plan data migration for **{{service_name}}** from **{{source_data_model}}**.

Use these references:
- `docs/architecture/microservice-principles.md`
- `.github/skills/sovereign-data-stores/SKILL.md`
- `docs/architecture/adr/0002-notification-service-boundary.md`
- `docs/architecture/adr/0003-student-service-data-ownership.md`

Output:
1. Service-owned schema boundary.
2. Data migration phases and cutover criteria.
3. Outbox/event publication strategy.
4. Read model/projection plan for cross-service queries.
