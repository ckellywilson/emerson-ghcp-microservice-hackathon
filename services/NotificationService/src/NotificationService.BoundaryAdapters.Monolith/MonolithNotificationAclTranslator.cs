using NotificationService.Application.Contracts.Api.V1;
using NotificationService.Application.Contracts.Events.V1;

namespace NotificationService.BoundaryAdapters.Monolith;

public sealed class MonolithNotificationAclTranslator : IMonolithNotificationAclTranslator
{
    public EntityChangedV1 ToCanonicalEvent(LegacyEntityChangedInput input)
    {
        var operation = input.Operation.ToUpperInvariant() switch
        {
            "CREATE" or "CREATED" => "CREATE",
            "UPDATE" or "UPDATED" => "UPDATE",
            "DELETE" or "DELETED" => "DELETE",
            _ => "UPDATE"
        };

        var eventId = Guid.NewGuid().ToString("N");

        return new EntityChangedV1(
            eventId,
            "ContosoUniversity.EntityChanged",
            "1.0",
            input.OccurredAt ?? DateTimeOffset.UtcNow,
            "ContosoUniversity.Monolith",
            input.CorrelationId ?? eventId,
            null,
            new EntityChangedPayload(
                input.EntityType,
                input.EntityId,
                input.EntityDisplayName,
                operation,
                input.UserName));
    }

    public LegacyNotificationResponse ToLegacyResponse(NotificationResponse response)
    {
        return new LegacyNotificationResponse(
            response.Id,
            response.EntityType,
            response.EntityId,
            response.Operation,
            response.Message,
            response.CreatedAt,
            response.CreatedBy,
            response.IsRead);
    }
}