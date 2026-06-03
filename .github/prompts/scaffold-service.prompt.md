---
description: Scaffold a new microservice shell from approved contracts while preserving stable interfaces and clear seams.
agent: "agent"
---

Scaffold **{{service_name}}** as a new service shell extracted from **{{source_system}}**.

Use these references:
- `docs/architecture/microservice-principles.md`
- `.github/instructions/microservice-extraction.instructions.md`
- `.github/copilot-instructions.md`

Output:
1. Solution and project structure for the new service shell.
2. Contract-first interfaces that remain stable against the approved API and event definitions.
3. Anti-corruption adapter placement at the monolith boundary.
4. Configuration wiring for local development and integration endpoints.
