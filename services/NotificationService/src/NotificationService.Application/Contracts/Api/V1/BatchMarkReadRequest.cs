namespace NotificationService.Application.Contracts.Api.V1;

public sealed record BatchMarkReadRequest(
    IReadOnlyCollection<string> Ids,
    DateTimeOffset? ReadAt);