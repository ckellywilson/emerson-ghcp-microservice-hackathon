---
marp: true
theme: default
paginate: true
footer: Emerson GHCP Workshop • Day 1 Showcase
style: |
  :root {
    --bg: #0d1117;
    --fg: #e6edf3;
    --muted: #8b949e;
    --copilot: #8534F3;
    --copilot-light: #C898FD;
    --accent: #58a6ff;
  }
  section {
    background: radial-gradient(circle at 10% 0%, #161b22 0%, var(--bg) 55%);
    color: var(--fg);
    font-family: "Segoe UI", system-ui, -apple-system, sans-serif;
    padding: 48px 64px;
  }
  h1, h2, h3 { color: var(--copilot-light); }
  h1 { color: var(--copilot); }
  strong { color: var(--copilot-light); }
  code { background: #161b22; color: #a5d6ff; padding: 0.1em 0.35em; border-radius: 6px; }
  pre code { display: block; padding: 12px; border-radius: 10px; border-left: 4px solid var(--copilot); }
  table { width: 100%; border-collapse: collapse; font-size: 0.9em; }
  th, td { border-bottom: 1px solid #30363d; padding: 8px; text-align: left; }
  th { color: var(--copilot-light); }
  blockquote { border-left: 4px solid var(--accent); padding-left: 12px; color: var(--muted); }
---

# Day 1 Showcase
## Research → Plan → Implement with GitHub Copilot

Monolith to Microservices: Notifications (Day 1) and Students (Day 2)

---

## Why This Workshop

- We start from a real brownfield app: `ContosoUniversity`
- Goal is **repeatable extraction**, not one-off refactoring
- Method: **Research → Plan → Implement (R-P-I)**

> Same workflow tomorrow for the Day 2 hackathon

---

## R-P-I Mapped to This Repo

| Phase | Evidence in Repo |
|---|---|
| Research | `docs/architecture/microservice-principles.md`, ADRs 0001-0003 |
| Plan | `.github/prompts/extract-microservice.prompt.md`, `.github/prompts/define-bounded-context.prompt.md`, `.github/prompts/design-service-api.prompt.md` |
| Implement | service skeletons + wiring/test prompts |
| Guardrails | `.github/copilot-instructions.md` + extraction instructions |

Plan-phase candidate analysis: Notification wins because the repo already has a clean `INotificationService` seam, Service Bus support, and almost no data to migrate. Student is the harder Day 2 target because `Enrollment -> Course` is more coupled.

---

## Why Notifications First

- Clean seam already exists via `INotificationService`
- Event integration already present (Service Bus seam)
- Minimal relational ownership = lower migration risk
- Removes the need to untangle `Student -> Enrollment -> Course` on Day 1
- Matches ADR-0002: cleanest seam first

---

## What We Build on Day 1

```text
NotificationService/
  Domain/
  Application/
  Api/
  Infrastructure/
  BoundaryAdapters.Monolith/
```

- Bounded context for Notification
- Anti-corruption mapping at monolith boundary
- Event-driven integration with idempotent handling
- Contract-first APIs and versioned event messages
- Sovereign data ownership, even if the carve-out is tiny

Expected output: a runnable Notification service slice that builds cleanly, respects the boundary, and can be demoed without touching the rest of the monolith.

---

## How We Build It (Live)

1. `/extract-microservice` -> extraction plan, rollback points, candidate rationale
2. `/define-bounded-context` -> aggregate root, invariants, glossary, context map
3. `/design-service-api` -> versioned HTTP contracts, errors, idempotency, ACL points
4. `/scaffold-service` -> service skeleton, `Program.cs`, `appsettings.json`, adapters
5. `/wire-event-seam` -> event contracts, Service Bus integration, idempotent handler
6. `/plan-data-migration` -> sovereign carve-out; for Notification, this stays trivial
7. `/test-strategy` -> contract tests, event idempotency tests, integration tests

---

## Live Demo Checkpoints

- Plan output explicitly picks Notification as the lowest-friction candidate
- Contracts are versioned before scaffolding
- Generated seams align to ADR decisions
- Build/test pass after slice implementation
- The monolith keeps working while the service slice proves the pattern

---

## Day 2 Preview: Student Service

- Harder coupling: `Student -> Enrollment -> Course`
- ADR-0003 decisions remove boundary ambiguity
- Student service owns student/enrollment lifecycle
- Course remains separate bounded context

---

## Branching and Tags for Workshop Delivery

- `main`: attendee baseline + latest presentation
- `rehearsal-day1-final` and `rehearsal-day2-final`: preserved historical refs
- `rehearsal-day1-final-v2` and `rehearsal-day2-final-v2`: updated final checkpoints

---

## Facilitator Runbook

```bash
git fetch --all --tags --prune
git checkout main
git pull --ff-only origin main
```

For completed examples:

```bash
# Choose one completed example at a time:
git checkout rehearsal-day1-final-v2
# or
git checkout rehearsal-day2-final-v2
```

---

## Takeaway

Copilot guidance plus architecture guardrails make extraction:

- faster
- safer
- easier to teach and repeat

Questions?
