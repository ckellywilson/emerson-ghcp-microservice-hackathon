namespace NotificationService.BoundaryAdapters.Monolith;

public sealed record LegacyNotificationResponse(
    string Id,
    string EntityType,
    string EntityId,
    string Type,
    string Message,
    DateTimeOffset CreatedAt,
    string? CreatedBy,
    bool IsRead);