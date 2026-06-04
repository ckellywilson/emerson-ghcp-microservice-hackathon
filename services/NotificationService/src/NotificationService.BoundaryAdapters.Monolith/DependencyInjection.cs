using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.BoundaryAdapters.Monolith;

public static class DependencyInjection
{
    public static IServiceCollection AddMonolithBoundaryAdapters(this IServiceCollection services)
    {
        services.AddSingleton<IMonolithNotificationAclTranslator, MonolithNotificationAclTranslator>();
        return services;
    }
}