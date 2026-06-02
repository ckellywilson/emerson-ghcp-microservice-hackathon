# Copilot Instructions for Microservice Extraction

When generating or refactoring code in this repository:

1. Apply [`docs/architecture/microservice-principles.md`](../docs/architecture/microservice-principles.md) as the canonical architectural standard.
2. Use DDD terms consistently: bounded contexts, aggregates, aggregate roots, ubiquitous language.
3. Prefer designs that maximize cohesion within a service and minimize runtime coupling between services.
4. Enforce sovereign data stores (database per service); never introduce shared-table coupling.
5. Prefer event-driven integration patterns with versioned contracts and idempotent consumers.
6. Add anti-corruption layers where monolith and service models differ.
7. For extraction tasks, use assets under:
   - `.github/skills/`
   - `.github/prompts/`
   - `.github/instructions/microservice-extraction.instructions.md`
