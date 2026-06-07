namespace StudentService.Application.Contracts.Events.V1;

public sealed record EnrollmentCreatedV1(
    string EventId,
    DateTimeOffset OccurredAt,
    int StudentId,
    int EnrollmentId,
    int CourseId,
    string? Grade,
    string Status);