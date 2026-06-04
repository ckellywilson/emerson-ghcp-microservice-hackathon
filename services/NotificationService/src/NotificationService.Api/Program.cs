using NotificationService.Application.Contracts.Api.V1;
using NotificationService.Application.Contracts.Events.V1;
using NotificationService.Application.Interfaces;
using NotificationService.Application;
using NotificationService.BoundaryAdapters.Monolith;
using NotificationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddNotificationApplication()
    .AddNotificationInfrastructure(builder.Configuration)
    .AddMonolithBoundaryAdapters();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/api/v1/health/ready", () =>
{
    return Results.Ok(new
    {
        status = "ready",
        checks = new
        {
            database = "ok",
            eventConsumer = "ok"
        }
    });
});

app.MapGet("/api/v1/notifications", async (
    bool? unreadOnly,
    string? entityType,
    string? operation,
    int? limit,
    string? cursor,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    var normalizedLimit = limit ?? 20;
    if (normalizedLimit is < 1 or > 100)
    {
        return Results.BadRequest(ErrorResponse.Validation("limit", "out_of_range", "limit must be between 1 and 100"));
    }

    var query = new NotificationQueryRequest(
        unreadOnly ?? false,
        entityType,
        operation,
        normalizedLimit,
        cursor);

    var response = await service.ListAsync(query, cancellationToken);
    return Results.Ok(response);
});

app.MapGet("/api/v1/notifications/{id}", async (
    string id,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    var notification = await service.GetByIdAsync(id, cancellationToken);
    return notification is null
        ? Results.NotFound(ErrorResponse.NotFound("notification_not_found", $"Notification {id} was not found."))
        : Results.Ok(notification);
});

app.MapPost("/api/v1/notifications/{id}/mark-read", async (
    string id,
    MarkReadRequest request,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    var result = await service.MarkReadAsync(id, request.ReadAt, cancellationToken);
    return result is null
        ? Results.NotFound(ErrorResponse.NotFound("notification_not_found", $"Notification {id} was not found."))
        : Results.Ok(result);
});

app.MapPost("/api/v1/notifications/mark-read-batch", async (
    BatchMarkReadRequest request,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    if (request.Ids.Count == 0 || request.Ids.Count > 100)
    {
        return Results.BadRequest(ErrorResponse.Validation("ids", "invalid_count", "ids must contain between 1 and 100 values"));
    }

    var result = await service.MarkReadBatchAsync(request, cancellationToken);
    return Results.Ok(result);
});

app.MapPost("/api/v1/events/entity-changed", async (
    EntityChangedV1 envelope,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    await service.AcceptInboundEventAsync(envelope, cancellationToken);
    return Results.Accepted();
});

app.MapPost("/acl/monolith/entity-changed", async (
    LegacyEntityChangedInput legacyInput,
    IMonolithNotificationAclTranslator translator,
    INotificationContractService service,
    CancellationToken cancellationToken) =>
{
    var canonicalEvent = translator.ToCanonicalEvent(legacyInput);
    await service.AcceptInboundEventAsync(canonicalEvent, cancellationToken);
    return Results.Accepted();
});

app.Run();

public partial class Program;
