using Microsoft.Extensions.DependencyInjection;

namespace StudentService.BoundaryAdapters.Monolith;

public static class DependencyInjection
{
    public static IServiceCollection AddMonolithBoundaryAdapters(this IServiceCollection services)
    {
        services.AddSingleton<IMonolithEnrollmentAclTranslator, MonolithEnrollmentAclTranslator>();
        return services;
    }
}