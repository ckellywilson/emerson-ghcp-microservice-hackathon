---
name: design-service-api
description: Produce contract-first API and event design aligned to bounded context and loose coupling principles.
---

Design service contracts for **{{service_name}}**.

Use these references:
- `docs/architecture/microservice-principles.md`
- `.github/instructions/microservice-extraction.instructions.md`
- `.github/skills/event-driven-integration/SKILL.md`

Output:
1. REST/HTTP endpoint contracts (request, response, errors).
2. Event contract definitions with versioning policy.
3. Idempotency and retry behavior.
4. ACL translation points for legacy callers.
