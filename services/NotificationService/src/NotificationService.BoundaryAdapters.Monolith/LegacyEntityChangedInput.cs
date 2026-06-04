namespace NotificationService.BoundaryAdapters.Monolith;

public sealed record LegacyEntityChangedInput(
    string EntityType,
    string EntityId,
    string? EntityDisplayName,
    string Operation,
    string? UserName,
    string? CorrelationId,
    DateTimeOffset? OccurredAt);