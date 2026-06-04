using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationApplication(this IServiceCollection services)
    {
        return services;
    }
}