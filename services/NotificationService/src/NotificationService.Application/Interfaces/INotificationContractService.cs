using NotificationService.Application.Contracts.Api.V1;
using NotificationService.Application.Contracts.Events.V1;

namespace NotificationService.Application.Interfaces;

public interface INotificationContractService
{
    Task<NotificationListResponse> ListAsync(NotificationQueryRequest request, CancellationToken cancellationToken);

    Task<NotificationResponse?> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<MarkReadResponse?> MarkReadAsync(string id, DateTimeOffset? readAt, CancellationToken cancellationToken);

    Task<BatchMarkReadResponse> MarkReadBatchAsync(BatchMarkReadRequest request, CancellationToken cancellationToken);

    Task AcceptInboundEventAsync(EntityChangedV1 envelope, CancellationToken cancellationToken);
}