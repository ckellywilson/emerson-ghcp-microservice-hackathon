---
name: event-driven-integration
description: Auto-activate when implementing async messaging, event contracts, idempotent handlers, or anti-corruption layers.
---

# Event-Driven Integration Skill

Use this for inter-service communication design.

## Heuristics
- Publish domain events at aggregate state transitions.
- Make event contracts explicit, versioned, and backward compatible.
- Enforce idempotency keys on consumers.
- Keep retries safe; handlers must be side-effect aware.
- Put anti-corruption adapters at boundaries to translate legacy models.

## Notification seam guidance
- Reuse the existing Service Bus seam for Notification extraction.
- Keep monolith producers stable while moving Notification consumption to the new service.

Reference: `docs/architecture/microservice-principles.md` and `docs/architecture/adr/0002-notification-service-boundary.md`.
