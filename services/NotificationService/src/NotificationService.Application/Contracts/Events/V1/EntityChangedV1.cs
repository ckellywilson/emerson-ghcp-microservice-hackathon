namespace NotificationService.Application.Contracts.Events.V1;

public sealed record EntityChangedV1(
    string EventId,
    string EventType,
    string EventVersion,
    DateTimeOffset OccurredAt,
    string SourceSystem,
    string CorrelationId,
    string? CausationId,
    EntityChangedPayload Payload);

public sealed record EntityChangedPayload(
    string EntityType,
    string EntityId,
    string? EntityDisplayName,
    string Operation,
    string? Actor);