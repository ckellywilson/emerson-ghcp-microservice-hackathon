namespace StudentService.Application.Contracts.Events.V1;

public sealed record CourseProjectionUpsertedV1(
    string EventId,
    DateTimeOffset OccurredAt,
    int CourseId,
    string CourseTitle,
    int Credits,
    bool IsActive);