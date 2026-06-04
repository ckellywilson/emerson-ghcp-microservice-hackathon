# Notification Extraction Walkthrough (Day 1)

This script demonstrates extraction of Notification as the first microservice using GHCP guidance assets.

## Step-to-asset map

| Step | Goal | GHCP asset(s) used |
|---|---|---|
| 1 | Plan extraction scope and rollout | [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md), [`.github/skills/microservice-decomposition/SKILL.md`](../../.github/skills/microservice-decomposition/SKILL.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 2 | Identify bounded context and ubiquitous language | [`.github/skills/ddd-bounded-contexts/SKILL.md`](../../.github/skills/ddd-bounded-contexts/SKILL.md), [`.github/prompts/define-bounded-context.prompt.md`](../../.github/prompts/define-bounded-context.prompt.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 3 | Design service API contracts first | [`.github/prompts/design-service-api.prompt.md`](../../.github/prompts/design-service-api.prompt.md), [`.github/instructions/microservice-extraction.instructions.md`](../../.github/instructions/microservice-extraction.instructions.md), [`../../docs/architecture/target-architecture.md`](../../docs/architecture/target-architecture.md) |
| 4 | Scaffold the service from contracts | [`.github/prompts/scaffold-service.prompt.md`](../../.github/prompts/scaffold-service.prompt.md), [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`.github/instructions/microservice-extraction.instructions.md`](../../.github/instructions/microservice-extraction.instructions.md) |
| 5 | Wire Service Bus event seam and ACL | [`.github/prompts/wire-event-seam.prompt.md`](../../.github/prompts/wire-event-seam.prompt.md), [`.github/skills/event-driven-integration/SKILL.md`](../../.github/skills/event-driven-integration/SKILL.md), [`../../docs/architecture/adr/0002-notification-service-boundary.md`](../../docs/architecture/adr/0002-notification-service-boundary.md) |
| 6 | Define sovereign datastore carve-out | [`.github/skills/sovereign-data-stores/SKILL.md`](../../.github/skills/sovereign-data-stores/SKILL.md), [`.github/prompts/plan-data-migration.prompt.md`](../../.github/prompts/plan-data-migration.prompt.md), [`../../docs/architecture/adr/0001-strangler-fig-extraction.md`](../../docs/architecture/adr/0001-strangler-fig-extraction.md) |
| 7 | Test strategy and quality gates | [`.github/prompts/test-strategy.prompt.md`](../../.github/prompts/test-strategy.prompt.md), [`.github/copilot-instructions.md`](../../.github/copilot-instructions.md), [`../../docs/architecture/microservice-principles.md`](../../docs/architecture/microservice-principles.md) |
| 8 | Code review and readiness decision | [`.github/prompts/extract-microservice.prompt.md`](../../.github/prompts/extract-microservice.prompt.md), [`../../docs/architecture/adr/0001-strangler-fig-extraction.md`](../../docs/architecture/adr/0001-strangler-fig-extraction.md) |

## Presenter script

### Step 1 — Plan
- Activate decomposition guidance.
- Run `/extract-microservice domain_name="Notification" source_system="ContosoUniversity monolith" target_service="NotificationService"`.
- Confirm strangler-fig increments and rollback points.
**Done when:**
  - Chat produced a written extraction plan covering the bounded context/aggregate boundary, strangler-fig phases with rollback points, API/event contracts, data migration, and a test/review checklist.
  - The `microservice-decomposition` skill activated and the plan treats Notification as a clean leaf-first seam.
  - No files are created in this step — the output is the plan in chat. This is expected.

### Step 2 — Define bounded context
- Activate `ddd-bounded-contexts` skill.
- Run `/define-bounded-context domain_name="Notification" system_name="ContosoUniversity"`.
- Document aggregate root, invariants, and ubiquitous language.
**Done when:**
  - You have a bounded-context statement with scope and non-goals, aggregate root(s) with invariants, a ubiquitous-language glossary, and a context map with integration edges.
  - No files are created — the output is the context analysis in chat. Expected.

### Step 3 — Design API (contract-first)
- Run `/design-service-api service_name="NotificationService"`.
- Validate API boundaries against high cohesion and low coupling rules.
**Done when:**
  - You have REST/HTTP endpoint contracts covering requests, responses, errors, versioned events, idempotency/retry behavior, and ACL translation points for legacy callers.
  - No code files are created yet — the output is the contract design in chat. Expected.

### Step 4 — Scaffold service
- Run `/scaffold-service service_name="NotificationService" source_system="ContosoUniversity monolith"`.
- Generate service shell from approved contracts.
- Keep interfaces stable and isolate monolith adapters.
**Done when:**
  - A new `NotificationService` project or solution exists outside `monolith/`, leaving the monolith unmodified.
  - The service reproduces the `INotificationService` contract, reuses the `Notification` and `EntityOperation` types from the approved design, and places an anti-corruption adapter at the monolith boundary.
  - Configuration wiring such as `Program.cs` and `appsettings.json` is present, and `dotnet build` for the new service succeeds.

### Step 5 — Wire event seam
- Run `/wire-event-seam service_name="NotificationService" source_system="ContosoUniversity monolith"`.
- Use existing Service Bus seam as integration boundary.
- Define explicit event contracts and idempotent handlers.
- Place anti-corruption adapters on the monolith edge.
**Done when:**
  - The service defines explicit, versioned event contracts and an idempotent inbound handler.
  - It reuses the existing Service Bus seam as the integration boundary so monolith producers remain stable.
  - Anti-corruption adapters translate legacy models at the monolith edge.

### Step 6 — Migrate to sovereign datastore
- Run `/plan-data-migration service_name="NotificationService" source_data_model="ContosoUniversity EF Core model"` for Notification-owned data.
- Note to presenter: Notification owns little to no relational data, so this carve-out is intentionally trivial for Day 1: avoid shared tables, keep ownership isolated, and consume/publish events rather than pulling Student-specific schema guidance into the walkthrough.
- Ensure no shared-table coupling remains.
**Done when:**
  - You've confirmed Notification owns little or no relational data, so the carve-out is intentionally trivial.
  - No shared-table coupling remains, ownership is isolated, and the service consumes and publishes events.
  - Typically no migration files are produced for Notification on Day 1. This is expected.

### Step 7 — Test
- Run `/test-strategy service_name="NotificationService"`.
- Validate contract tests, event handler idempotency, and integration behavior.
**Done when:**
  - Contract tests, event-handler idempotency tests, and integration  exist for the service.
  - A fresh-clone `dotnet build` passes as the smoke test, matching the repo CI flow of `dotnet restore`, `dotnet build`, and `dotnet test` on PRs.

### Step 8 — Review
- Run review pass against principles and ADR decisions.
- Approve rollout slice or loop for refinements.
**Done when:**
  - The rollout slice has been checked against `docs/architecture/microservice-principles.md` and the ADRs `0001-strangler-fig-extraction` and `0002-notification-service-boundary`.
  - A clear decision is made to approve the slice or loop back for refinement.
  - No files are created in this step — the output is the review decision in chat. This is expected.
