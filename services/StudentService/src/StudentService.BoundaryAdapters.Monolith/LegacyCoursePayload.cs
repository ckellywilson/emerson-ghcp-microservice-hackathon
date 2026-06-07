namespace StudentService.BoundaryAdapters.Monolith;

public sealed record LegacyCoursePayload(
    int CourseId,
    string Title,
    int Credits,
    bool IsActive,
    DateTimeOffset OccurredAt,
    string CorrelationId);