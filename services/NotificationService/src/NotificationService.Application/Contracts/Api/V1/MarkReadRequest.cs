namespace NotificationService.Application.Contracts.Api.V1;

public sealed record MarkReadRequest(
    DateTimeOffset? ReadAt);