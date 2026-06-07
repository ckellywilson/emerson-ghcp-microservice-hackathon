namespace StudentService.Infrastructure.Configuration;

public sealed class IntegrationEndpointsOptions
{
    public const string SectionName = "IntegrationEndpoints";

    public string CourseProjectionTopic { get; set; } = "course-projection-upserted-v1";

    public string StudentEventsTopic { get; set; } = "student-events-v1";
}