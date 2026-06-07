using StudentService.Application.Contracts.Events.V1;

namespace StudentService.BoundaryAdapters.Monolith;

public interface IMonolithEnrollmentAclTranslator
{
    CourseProjectionUpsertedV1 TranslateCourseProjection(LegacyCoursePayload payload);
}