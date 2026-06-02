# ADR 0001: Strangler-Fig Extraction Strategy

## Status
Accepted

## Context
The workshop starts from a brownfield monolith and needs a low-risk extraction sequence that can be demonstrated and repeated by teams.

## Decision
Adopt a strangler-fig strategy: extract capabilities in thin vertical slices, route traffic to new services incrementally, and keep rollback points at each step.

## Consequences
- Reduced migration risk versus big-bang decomposition.
- Requires temporary coexistence and boundary adapters.
- Demands disciplined contract/version management across slices.
