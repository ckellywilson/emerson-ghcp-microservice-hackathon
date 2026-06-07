using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Application.Interfaces;
using StudentService.Infrastructure.Configuration;
using StudentService.Infrastructure.Data;
using StudentService.Infrastructure.Services;

namespace StudentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("StudentServiceDb")
            ?? "Data Source=studentservice.db";

        services.AddDbContext<StudentServiceDbContext>(options =>
            options.UseSqlite(connectionString));

        services.Configure<IntegrationEndpointsOptions>(configuration.GetSection(IntegrationEndpointsOptions.SectionName));
        services.AddScoped<SqliteStudentService>();
        services.AddScoped<IStudentCommandService>(sp => sp.GetRequiredService<SqliteStudentService>());
        services.AddScoped<IStudentQueryService>(sp => sp.GetRequiredService<SqliteStudentService>());
        return services;
    }
}