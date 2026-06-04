namespace NotificationService.Infrastructure.Configuration;

public sealed class IntegrationEndpointsOptions
{
    public const string SectionName = "IntegrationEndpoints";

    public string MonolithAclIngressPath { get; set; } = "/acl/monolith/entity-changed";

    public string CanonicalIngressPath { get; set; } = "/api/v1/events/entity-changed";
}