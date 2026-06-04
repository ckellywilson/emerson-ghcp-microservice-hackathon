namespace NotificationService.Domain.ValueObjects;

public sealed record NotificationOrigin(
    string EntityType,
    string EntityId,
    string? EntityDisplayName);