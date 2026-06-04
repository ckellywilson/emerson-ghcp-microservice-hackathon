namespace NotificationService.Application.Contracts.Api.V1;

public sealed record BatchMarkReadResponse(
    int Updated,
    IReadOnlyCollection<string> NotFound,
    IReadOnlyCollection<string> AlreadyRead);