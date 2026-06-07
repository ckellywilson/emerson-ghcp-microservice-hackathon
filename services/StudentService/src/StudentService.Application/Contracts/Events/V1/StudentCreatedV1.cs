namespace StudentService.Application.Contracts.Events.V1;

public sealed record StudentCreatedV1(
    string EventId,
    DateTimeOffset OccurredAt,
    int StudentId,
    string LastName,
    string FirstMidName,
    DateOnly EnrollmentDate);