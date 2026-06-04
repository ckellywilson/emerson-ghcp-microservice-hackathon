namespace NotificationService.Application.Contracts.Events.V1;

public sealed record NotificationReadStateChangedV1(
    string EventId,
    string EventType,
    string EventVersion,
    DateTimeOffset OccurredAt,
    string SourceSystem,
    string CorrelationId,
    NotificationReadStateChangedPayload Payload);

public sealed record NotificationReadStateChangedPayload(
    string NotificationId,
    bool IsRead,
    DateTimeOffset? ReadAt,
    string? ReadBy);