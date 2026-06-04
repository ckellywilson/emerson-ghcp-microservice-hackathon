using NotificationService.Domain.Enums;
using NotificationService.Domain.ValueObjects;

namespace NotificationService.Domain.Aggregates;

public sealed class NotificationItem
{
    public string Id { get; private set; }

    public NotificationOrigin Origin { get; private set; }

    public NotificationOperation Operation { get; private set; }

    public string Message { get; private set; }

    public string Title { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public string? CreatedBy { get; private set; }

    public bool IsRead { get; private set; }

    public DateTimeOffset? ReadAt { get; private set; }

    public string CorrelationId { get; private set; }

    public string SourceSystem { get; private set; }

    public string SchemaVersion { get; private set; }

    public string EventId { get; private set; }

    public NotificationItem(
        string id,
        NotificationOrigin origin,
        NotificationOperation operation,
        string message,
        string title,
        DateTimeOffset createdAt,
        string? createdBy,
        string correlationId,
        string sourceSystem,
        string schemaVersion,
        string eventId)
    {
        Id = id;
        Origin = origin;
        Operation = operation;
        Message = message;
        Title = title;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        CorrelationId = correlationId;
        SourceSystem = sourceSystem;
        SchemaVersion = schemaVersion;
        EventId = eventId;
    }

    public bool MarkRead(DateTimeOffset readAt)
    {
        if (IsRead)
        {
            return false;
        }

        IsRead = true;
        ReadAt = readAt;
        return true;
    }
}