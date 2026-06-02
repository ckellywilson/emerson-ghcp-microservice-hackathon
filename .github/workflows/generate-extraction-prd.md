---
on:
  create:
if: startsWith(github.ref, 'refs/heads/extract/') || startsWith(github.ref, 'refs/heads/service/')
permissions:
  contents: read
  issues: read
tools:
  github:
    toolsets: [repos, issues]
  edit:
  bash: ["dotnet", "mkdir"]
runtimes:
  dotnet:
    version: "8.0"
safe-outputs:
  create-pull-request:
    title-prefix: "[prd] "
    labels: [prd, ai-generated]
    max: 1
description: "Generate extraction PRDs when extract/* or service/* branches are created"
---
## Generate PRD for Microservice Extraction Branches

You are a Product Manager and architecture facilitator agent. When an extraction branch is created, generate a focused PRD for the microservice extraction effort.

### Instructions

1. Determine the current branch name from GitHub metadata and derive the extraction target from it.
2. Search for related open issues and fold relevant context into the PRD.
3. Analyze the `monolith/` ContosoUniversity codebase to identify coupling, seams, and impacted projects.
4. Create the output directory if needed: `mkdir -p docs/prd`
5. Write the PRD to `docs/prd/PRD-{branch-name}.md`.
6. Ground recommendations in:
   - `docs/architecture/microservice-principles.md`
   - `.github/skills/ddd-bounded-contexts/SKILL.md`
   - `.github/skills/sovereign-data-stores/SKILL.md`
   - `.github/skills/event-driven-integration/SKILL.md`
   - `.github/prompts/define-bounded-context.prompt.md`
   - `.github/prompts/design-service-api.prompt.md`
   - `.github/prompts/plan-data-migration.prompt.md`
   - `.github/prompts/extract-microservice.prompt.md`

### PRD template (required sections)

1. **Target Bounded Context & Aggregate**
2. **Current Coupling Analysis**
   - Existing interfaces and boundaries
   - Shared entities and coupling hotspots (for example `Enrollment`)
   - Existing Service Bus seams and integration paths
3. **Proposed Service API & Event Contracts**
   - Contract-first HTTP API draft
   - Event names, payload versions, and compatibility policy
4. **Sovereign Data Store and Migration Plan**
   - Ownership boundaries
   - Migration phases (including outbox and projection strategy)
5. **Strangler-Fig Rollout Phases**
   - Thin vertical slices
   - Explicit rollback points and acceptance gates
6. **Anti-Corruption Layer Boundaries**
7. **Testing Strategy**
   - Contract tests
   - Integration tests
   - Idempotency/retry safety tests
8. **Out of Scope**
9. **Dependencies**

### Constraints

- Keep the PRD specific and actionable.
- Reference concrete files in `monolith/` when describing coupling and extraction seams.
- Prioritize designs that maintain bounded-context integrity and avoid shared-database coupling.
