# NotificationService Shell

This folder contains a contract-first shell for extracting the Notification capability from the ContosoUniversity monolith.

## Solution structure

- `NotificationService.slnx`
- `src/NotificationService.Api`
  - HTTP endpoints (`/api/v1/*`) for notification read and acknowledge workflows.
  - Ingress endpoints for canonical events and monolith ACL events.
- `src/NotificationService.Application`
  - Stable API contracts and event schemas (`Contracts/Api/V1`, `Contracts/Events/V1`).
  - Service interface (`INotificationContractService`) for use-case orchestration.
- `src/NotificationService.Domain`
  - Aggregate root `NotificationItem` and value object `NotificationOrigin`.
  - Invariants for immutable origin data and idempotent read transition.
- `src/NotificationService.Infrastructure`
  - Local in-memory implementation for service shell behavior.
  - Configuration options for service bus and integration endpoint wiring.
- `src/NotificationService.BoundaryAdapters.Monolith`
  - Anti-corruption translation for legacy monolith payloads to canonical events.
  - Legacy response mapping to support strangler compatibility.

## Configuration wiring

The API reads these sections from appsettings:

- `NotificationService` for page-size behavior.
- `ServiceBus` for integration backbone settings.
- `IntegrationEndpoints` for canonical and monolith ACL ingress paths.

## Local run

```bash
dotnet build services/NotificationService/NotificationService.slnx
dotnet run --project services/NotificationService/src/NotificationService.Api/NotificationService.Api.csproj
```