namespace StudentService.Application.Contracts.Api.V1;

public sealed record EnrollmentResponse(
    int EnrollmentId,
    int StudentId,
    int CourseId,
    string CourseTitle,
    string? Grade,
    string Status,
    string ProjectionStatus);