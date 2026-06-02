---
applyTo: "**/*"
---

# Microservice Extraction Guidance

For extraction work in this repository:

- Start from [`docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md).
- Use bounded-context first analysis before code movement.
- Keep service APIs contract-first and explicitly versioned.
- Enforce database-per-service ownership and avoid shared tables.
- Prefer asynchronous event integration and idempotent consumers.
- Place anti-corruption mappings at monolith/service boundaries.
- Follow strangler-fig increments with rollback-safe milestones.
