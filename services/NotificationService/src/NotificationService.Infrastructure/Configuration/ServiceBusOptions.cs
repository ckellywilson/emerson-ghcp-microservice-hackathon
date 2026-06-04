namespace NotificationService.Infrastructure.Configuration;

public sealed class ServiceBusOptions
{
    public const string SectionName = "ServiceBus";

    public string ConnectionString { get; set; } = string.Empty;

    public string InboundTopic { get; set; } = "contoso.domain.entity-changed.v1";

    public string OutboundTopic { get; set; } = "notification.read-state-changed.v1";
}