# Notification Extraction Walkthrough (Day 1)

This script demonstrates extraction of Notification as the first microservice using GHCP guidance assets.

## Step-to-asset map

| Step | Goal | GHCP asset(s) used |
|---|---|---|
| 1 | Plan extraction scope and rollout | [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md), [`.github/skills/microservice-decomposition/SKILL.md`](../../.github/skills/microservice-decomposition/SKILL.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 2 | Identify bounded context and ubiquitous language | [`.github/skills/ddd-bounded-contexts/SKILL.md`](../../.github/skills/ddd-bounded-contexts/SKILL.md), [`.github/prompts/define-bounded-context.prompt.md`](../../.github/prompts/define-bounded-context.prompt.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 3 | Design service API contracts first | [`.github/prompts/design-service-api.prompt.md`](../../.github/prompts/design-service-api.prompt.md), [`.github/instructions/microservice-extraction.instructions.md`](../../.github/instructions/microservice-extraction.instructions.md), [`../../docs/architecture/target-architecture.md`](../../docs/architecture/target-architecture.md) |
| 4 | Scaffold the service from contracts | [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md) |
| 5 | Wire Service Bus event seam and ACL | [`.github/skills/event-driven-integration/SKILL.md`](../../.github/skills/event-driven-integration/SKILL.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md), [`../../docs/architecture/adr/0002-notification-service-boundary.md`](../../docs/architecture/adr/0002-notification-service-boundary.md) |
| 6 | Define sovereign datastore carve-out | [`.github/skills/sovereign-data-stores/SKILL.md`](../../.github/skills/sovereign-data-stores/SKILL.md), [`.github/prompts/plan-data-migration.prompt.md`](../../.github/prompts/plan-data-migration.prompt.md), [`../../docs/architecture/adr/0001-strangler-fig-extraction.md`](../../docs/architecture/adr/0001-strangler-fig-extraction.md) |
| 7 | Test strategy and quality gates | [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 8 | Code review and readiness decision | [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md), [`../../docs/architecture/adr/0001-strangler-fig-extraction.md`](../../docs/architecture/adr/0001-strangler-fig-extraction.md) |

## Presenter script

### Step 1 — Plan
- Activate decomposition guidance.
- Run `/extract-microservice` with domain=`Notification`.
- Confirm strangler-fig increments and rollback points.

### Step 2 — Define bounded context
- Activate `ddd-bounded-contexts` skill.
- Run `/define-bounded-context` for Notification.
- Document aggregate root, invariants, and ubiquitous language.

### Step 3 — Design API (contract-first)
- Run `/design-service-api`.
- Validate API boundaries against high cohesion and low coupling rules.

### Step 4 — Scaffold service
- Generate service shell from approved contracts.
- Keep interfaces stable and isolate monolith adapters.

### Step 5 — Wire event seam
- Use existing Service Bus seam as integration boundary.
- Define explicit event contracts and idempotent handlers.
- Place anti-corruption adapters on the monolith edge.

### Step 6 — Migrate to sovereign datastore
- Run `/plan-data-migration` for Notification-owned data.
- Ensure no shared-table coupling remains.

### Step 7 — Test
- Validate contract tests, event handler idempotency, and integration behavior.

### Step 8 — Review
- Run review pass against principles and ADR decisions.
- Approve rollout slice or loop for refinements.
