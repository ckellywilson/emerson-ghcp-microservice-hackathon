# Emerson GHCP Microservice Hackathon

This repository hosts a 2-day GitHub Copilot (GHCP) workshop for Emerson focused on extracting microservices from a brownfield monolith using agentic best practices.

## Purpose

- **Day 1 (showcase):** presenter-led extraction of the **Notification** domain from ContosoUniversity.
- **Day 2 (hackathon):** team-based extraction of the **Student** aggregate, including data-ownership decisions around `Enrollment`.

## 2-Day Agenda

### Day 1: Notification extraction showcase
1. Plan extraction scope and risks.
2. Define bounded context and service boundary.
3. Design API contracts first.
4. Scaffold service and wire event integration.
5. Plan sovereign datastore migration.
6. Test and review.

### Day 2: Student extraction hackathon
1. Analyze Student/Course/Enrollment coupling.
2. Define ownership and integration strategy.
3. Implement decomposition plan with GHCP prompts and skills.
4. Validate architecture, tests, and operational readiness.
5. Present trade-offs and outcomes.

## Prerequisites

- GitHub Copilot Business or Enterprise with agent mode enabled
- GitHub Actions enabled
- GitHub Codespaces access (or equivalent local dev environment)
- MCP-capable GHCP setup for docs/context integrations
- .NET 8 development tooling for monolith and service extraction exercises

## Repository Structure

- [`monolith/`](monolith/README.md): source/reference monolith context and placement notes.
- [`showcase/`](showcase/README.md): Day-1 presenter-led materials.
- [`hackathon/`](hackathon/README.md): Day-2 challenge materials.
- [`docs/architecture/`](docs/architecture/): canonical architecture principles, topology, and ADRs.
- [`.github/`](.github/): GHCP instructions, prompts, and skills used during extraction.

## GHCP Guidance Assets

Use these assets together to enforce DDD, loose coupling, high cohesion, sovereign data stores, event-driven communication, and anti-corruption layers.

### Architecture references
- [`docs/architecture/microservice-principles.md`](docs/architecture/microservice-principles.md) (canonical principles)
- [`docs/architecture/target-architecture.md`](docs/architecture/target-architecture.md)
- ADRs:
  - [`0001-strangler-fig-extraction.md`](docs/architecture/adr/0001-strangler-fig-extraction.md)
  - [`0002-notification-service-boundary.md`](docs/architecture/adr/0002-notification-service-boundary.md)
  - [`0003-student-service-data-ownership.md`](docs/architecture/adr/0003-student-service-data-ownership.md)

### Copilot instructions
- [`.github/copilot-instructions.md`](.github/copilot-instructions.md)
- [`.github/instructions/microservice-extraction.instructions.md`](.github/instructions/microservice-extraction.instructions.md)

### Reusable prompts
- [`.github/prompts/extract-microservice.prompt.md`](.github/prompts/extract-microservice.prompt.md)
- [`.github/prompts/define-bounded-context.prompt.md`](.github/prompts/define-bounded-context.prompt.md)
- [`.github/prompts/design-service-api.prompt.md`](.github/prompts/design-service-api.prompt.md)
- [`.github/prompts/plan-data-migration.prompt.md`](.github/prompts/plan-data-migration.prompt.md)

### Auto-activating skills
- [`.github/skills/ddd-bounded-contexts/SKILL.md`](.github/skills/ddd-bounded-contexts/SKILL.md)
- [`.github/skills/microservice-decomposition/SKILL.md`](.github/skills/microservice-decomposition/SKILL.md)
- [`.github/skills/sovereign-data-stores/SKILL.md`](.github/skills/sovereign-data-stores/SKILL.md)
- [`.github/skills/event-driven-integration/SKILL.md`](.github/skills/event-driven-integration/SKILL.md)

## How to use this repository

1. Read [`docs/architecture/microservice-principles.md`](docs/architecture/microservice-principles.md).
2. Follow the Day-1 walkthrough in [`showcase/docs/notification-extraction-walkthrough.md`](showcase/docs/notification-extraction-walkthrough.md).
3. Run the Day-2 challenge using [`hackathon/docs/student-service-challenge.md`](hackathon/docs/student-service-challenge.md).
4. Use prompts and skills from `.github/` to keep generation aligned with the architecture standards.

## Facilitator Rehearsal Handoff

- Use [`showcase/docs/rehearsal-handoff.md`](showcase/docs/rehearsal-handoff.md) for reset/runbook commands.
- Immutable finished rehearsal tags:
  - `rehearsal-day1-final`
  - `rehearsal-day2-final`
