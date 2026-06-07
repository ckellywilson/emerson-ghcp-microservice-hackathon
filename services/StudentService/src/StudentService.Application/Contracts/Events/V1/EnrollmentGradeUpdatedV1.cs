namespace StudentService.Application.Contracts.Events.V1;

public sealed record EnrollmentGradeUpdatedV1(
    string EventId,
    DateTimeOffset OccurredAt,
    int StudentId,
    int EnrollmentId,
    int CourseId,
    string Grade,
    string Status);