using StudentService.Application.Contracts.Events.V1;

namespace StudentService.BoundaryAdapters.Monolith;

public sealed class MonolithEnrollmentAclTranslator : IMonolithEnrollmentAclTranslator
{
    public CourseProjectionUpsertedV1 TranslateCourseProjection(LegacyCoursePayload payload)
    {
        return new CourseProjectionUpsertedV1(
            payload.CorrelationId,
            payload.OccurredAt,
            payload.CourseId,
            payload.Title,
            payload.Credits,
            payload.IsActive);
    }
}