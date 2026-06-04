namespace NotificationService.Application.Contracts.Api.V1;

public sealed record MarkReadResponse(
    string Id,
    bool IsRead,
    DateTimeOffset? ReadAt);