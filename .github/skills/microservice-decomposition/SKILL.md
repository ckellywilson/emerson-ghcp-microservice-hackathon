---
name: microservice-decomposition
description: Auto-activate when decomposing monolith modules, evaluating extraction seams, or planning strangler-fig migrations.
---

# Microservice Decomposition Skill

Use this for extraction planning and seam selection.

## Heuristics
- Extract the leaf first: prefer domains with clear interfaces and minimal relational coupling.
- Favor async boundaries already present (queues, events, pub/sub).
- Start with seams that avoid distributed transactions.
- Keep slices vertical (API + domain + persistence + integration) and incremental.
- Record rollback criteria per slice.

## Workshop-specific guidance
- **Notification** is a clean seam: interface-backed and event-oriented.
- **Student** is coupled via `Enrollment`: treat as an architectural decision exercise.

Reference: `docs/architecture/microservice-principles.md` and `docs/architecture/adr/0001-strangler-fig-extraction.md`.
