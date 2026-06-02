# Target Architecture

## Topology

- **Monolith (legacy host):** gradually reduced as capabilities are extracted.
- **Notification Service:** first extracted service; consumes and serves notification workflows.
- **Student Service:** second extraction target with explicit ownership decisions around enrollment-related workflows.
- **Integration backbone:** asynchronous messaging (Service Bus) for cross-service events.

## Service boundaries

- Notification: notification lifecycle, read/unread workflows, notification delivery metadata.
- Student: student profile/lifecycle and student-owned operations.
- Course domain remains separate; cross-domain interactions via events and ACL adapters.

## Communication patterns

- Prefer async domain events for state propagation.
- Use sync APIs only when immediate consistency is required and bounded.
- Version all public APIs and event contracts.

## Data boundaries

- Database-per-service model.
- No shared-table joins across service boundaries.
- Use projections/read models for query composition.

Canonical principle reference: [`microservice-principles.md`](microservice-principles.md).
