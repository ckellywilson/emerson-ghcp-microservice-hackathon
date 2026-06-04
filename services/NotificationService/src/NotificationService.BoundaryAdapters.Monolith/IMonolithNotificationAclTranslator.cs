using NotificationService.Application.Contracts.Api.V1;
using NotificationService.Application.Contracts.Events.V1;

namespace NotificationService.BoundaryAdapters.Monolith;

public interface IMonolithNotificationAclTranslator
{
    EntityChangedV1 ToCanonicalEvent(LegacyEntityChangedInput input);

    LegacyNotificationResponse ToLegacyResponse(NotificationResponse response);
}