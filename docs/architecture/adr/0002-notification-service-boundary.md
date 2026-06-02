# ADR 0002: Notification Service Boundary

## Status
Accepted

## Context
Notification behavior in the monolith already has a clear interface seam (`INotificationService`) and event-oriented integration, making it an ideal first extraction target.

## Decision
Define Notification as an independent bounded context and extract it first as a standalone service with event-driven integration and ACL adapters at monolith boundaries.

## Consequences
- Provides a low-friction showcase extraction for Day 1.
- Establishes reusable patterns for contracts, events, and idempotent consumers.
- Requires explicit ownership of Notification data in a service-local datastore.
