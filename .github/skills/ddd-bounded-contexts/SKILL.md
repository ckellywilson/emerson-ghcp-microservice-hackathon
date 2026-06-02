---
name: ddd-bounded-contexts
description: Auto-activate when requests involve domain modeling, service boundaries, aggregates, or context mapping for extraction.
---

# DDD Bounded Contexts Skill

Use this when identifying what belongs inside a service.

## Heuristics
- Start from domain language in use cases, not tables.
- Boundary test: if a rule must be transactionally consistent, keep it in one aggregate.
- Choose one aggregate root per consistency boundary.
- Mark terms with overloaded meanings and split contexts when meanings diverge.
- Define upstream/downstream context relationships before API design.

## Output checklist
- Bounded context statement (scope and non-goals)
- Aggregate root list with invariants
- Ubiquitous language glossary
- Context map with integration edges

Reference: `docs/architecture/microservice-principles.md`.
