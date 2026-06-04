namespace NotificationService.Application.Contracts.Api.V1;

public sealed record NotificationResponse(
    string Id,
    string EntityType,
    string EntityId,
    string? EntityDisplayName,
    string Operation,
    string Title,
    string Message,
    DateTimeOffset CreatedAt,
    string? CreatedBy,
    bool IsRead,
    DateTimeOffset? ReadAt,
    string CorrelationId,
    string SourceSystem,
    string SchemaVersion);