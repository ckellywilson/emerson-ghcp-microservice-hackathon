using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces;
using NotificationService.Infrastructure.Configuration;
using NotificationService.Infrastructure.Services;

namespace NotificationService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NotificationServiceOptions>(
            configuration.GetSection(NotificationServiceOptions.SectionName));

        services.Configure<ServiceBusOptions>(
            configuration.GetSection(ServiceBusOptions.SectionName));

        services.Configure<IntegrationEndpointsOptions>(
            configuration.GetSection(IntegrationEndpointsOptions.SectionName));

        services.AddSingleton<INotificationContractService, InMemoryNotificationContractService>();

        return services;
    }
}