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
| Plan | `.github/prompts/*` contract-first and decomposition prompts |
| Implement | service skeletons + wiring/test prompts |
| Guardrails | `.github/copilot-instructions.md` + extraction instructions |

---

## Why Notifications First

- Clean seam already exists via `INotificationService`
- Event integration already present (Service Bus seam)
- Minimal relational ownership = lower migration risk
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

---

## How We Build It (Live)

1. `/extract-microservice`
2. `/define-bounded-context`
3. `/design-service-api`
4. `/scaffold-service`
5. `/wire-event-seam`
6. `/test-strategy`

---

## Live Demo Checkpoints

- Research output in chat is explicit and reusable
- Contracts are versioned before scaffolding
- Generated seams align to ADR decisions
- Build/test pass after slice implementation

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
git checkout rehearsal-day1-final-v2
git checkout rehearsal-day2-final-v2
```

---

## Takeaway

Copilot guidance plus architecture guardrails makes extraction:

- faster
- safer
- easier to teach and repeat

Questions?
