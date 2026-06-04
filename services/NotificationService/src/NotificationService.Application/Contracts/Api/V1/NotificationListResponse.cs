namespace NotificationService.Application.Contracts.Api.V1;

public sealed record NotificationListResponse(
    IReadOnlyCollection<NotificationResponse> Items,
    NotificationPage Page);

public sealed record NotificationPage(
    string? NextCursor,
    int Limit);