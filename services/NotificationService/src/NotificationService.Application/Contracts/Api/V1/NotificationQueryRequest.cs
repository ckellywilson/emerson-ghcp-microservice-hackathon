namespace NotificationService.Application.Contracts.Api.V1;

public sealed record NotificationQueryRequest(
    bool UnreadOnly,
    string? EntityType,
    string? Operation,
    int Limit,
    string? Cursor);