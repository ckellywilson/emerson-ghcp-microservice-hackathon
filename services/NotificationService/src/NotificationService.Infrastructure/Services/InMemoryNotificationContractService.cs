using System.Collections.Concurrent;
using NotificationService.Application.Contracts.Api.V1;
using NotificationService.Application.Contracts.Events.V1;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Aggregates;
using NotificationService.Domain.Enums;
using NotificationService.Domain.ValueObjects;

namespace NotificationService.Infrastructure.Services;

public sealed class InMemoryNotificationContractService : INotificationContractService
{
    private readonly ConcurrentDictionary<string, NotificationItem> _notifications = new();
    private readonly ConcurrentDictionary<string, byte> _processedEvents = new();

    public Task<NotificationListResponse> ListAsync(NotificationQueryRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<NotificationItem> query = _notifications.Values;

        if (request.UnreadOnly)
        {
            query = query.Where(n => !n.IsRead);
        }

        if (!string.IsNullOrWhiteSpace(request.EntityType))
        {
            query = query.Where(n => string.Equals(n.Origin.EntityType, request.EntityType, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(request.Operation) && TryParseOperation(request.Operation, out var operation))
        {
            query = query.Where(n => n.Operation == operation);
        }

        var items = query
            .OrderByDescending(n => n.CreatedAt)
            .Take(request.Limit)
            .Select(MapToResponse)
            .ToArray();

        var response = new NotificationListResponse(items, new NotificationPage(null, request.Limit));
        return Task.FromResult(response);
    }

    public Task<NotificationResponse?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var exists = _notifications.TryGetValue(id, out var item);
        return Task.FromResult(exists ? MapToResponse(item!) : null);
    }

    public Task<MarkReadResponse?> MarkReadAsync(string id, DateTimeOffset? readAt, CancellationToken cancellationToken)
    {
        if (!_notifications.TryGetValue(id, out var item))
        {
            return Task.FromResult<MarkReadResponse?>(null);
        }

        var effectiveReadAt = readAt ?? DateTimeOffset.UtcNow;
        item.MarkRead(effectiveReadAt);

        var response = new MarkReadResponse(item.Id, item.IsRead, item.ReadAt);
        return Task.FromResult<MarkReadResponse?>(response);
    }

    public async Task<BatchMarkReadResponse> MarkReadBatchAsync(BatchMarkReadRequest request, CancellationToken cancellationToken)
    {
        var updated = 0;
        var notFound = new List<string>();
        var alreadyRead = new List<string>();

        foreach (var id in request.Ids.Distinct(StringComparer.Ordinal))
        {
            var existing = await MarkReadAsync(id, request.ReadAt, cancellationToken);
            if (existing is null)
            {
                notFound.Add(id);
                continue;
            }

            if (existing.ReadAt == request.ReadAt || request.ReadAt is null)
            {
                updated++;
            }
            else
            {
                alreadyRead.Add(id);
            }
        }

        return new BatchMarkReadResponse(updated, notFound, alreadyRead);
    }

    public Task AcceptInboundEventAsync(EntityChangedV1 envelope, CancellationToken cancellationToken)
    {
        if (!_processedEvents.TryAdd(envelope.EventId, 0))
        {
            return Task.CompletedTask;
        }

        if (!TryParseOperation(envelope.Payload.Operation, out var operation))
        {
            operation = NotificationOperation.Update;
        }

        var id = $"ntf_{envelope.EventId}";
        var origin = new NotificationOrigin(envelope.Payload.EntityType, envelope.Payload.EntityId, envelope.Payload.EntityDisplayName);
        var title = BuildTitle(origin.EntityType, operation);
        var message = BuildMessage(origin, operation);

        var item = new NotificationItem(
            id,
            origin,
            operation,
            message,
            title,
            envelope.OccurredAt,
            envelope.Payload.Actor,
            envelope.CorrelationId,
            envelope.SourceSystem,
            envelope.EventVersion,
            envelope.EventId);

        _notifications[id] = item;
        return Task.CompletedTask;
    }

    private static NotificationResponse MapToResponse(NotificationItem item)
    {
        return new NotificationResponse(
            item.Id,
            item.Origin.EntityType,
            item.Origin.EntityId,
            item.Origin.EntityDisplayName,
            item.Operation.ToString().ToUpperInvariant(),
            item.Title,
            item.Message,
            item.CreatedAt,
            item.CreatedBy,
            item.IsRead,
            item.ReadAt,
            item.CorrelationId,
            item.SourceSystem,
            item.SchemaVersion);
    }

    private static bool TryParseOperation(string operation, out NotificationOperation result)
    {
        return operation.ToUpperInvariant() switch
        {
            "CREATE" => Parse(NotificationOperation.Create, out result),
            "UPDATE" => Parse(NotificationOperation.Update, out result),
            "DELETE" => Parse(NotificationOperation.Delete, out result),
            _ => Parse(NotificationOperation.Update, out result, false)
        };
    }

    private static bool Parse(NotificationOperation value, out NotificationOperation result, bool success = true)
    {
        result = value;
        return success;
    }

    private static string BuildTitle(string entityType, NotificationOperation operation)
    {
        return operation switch
        {
            NotificationOperation.Create => $"New {entityType}",
            NotificationOperation.Update => $"{entityType} Updated",
            NotificationOperation.Delete => $"{entityType} Deleted",
            _ => $"{entityType} Notification"
        };
    }

    private static string BuildMessage(NotificationOrigin origin, NotificationOperation operation)
    {
        var subject = string.IsNullOrWhiteSpace(origin.EntityDisplayName)
            ? $"{origin.EntityType} ({origin.EntityId})"
            : $"{origin.EntityType} {origin.EntityDisplayName}";

        return operation switch
        {
            NotificationOperation.Create => $"{subject} was created.",
            NotificationOperation.Update => $"{subject} was updated.",
            NotificationOperation.Delete => $"{subject} was deleted.",
            _ => $"{subject} changed."
        };
    }
}