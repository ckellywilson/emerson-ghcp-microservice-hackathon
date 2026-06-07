namespace StudentService.Application.Contracts.Api.V1;

public sealed record StudentResponse(
    int StudentId,
    string LastName,
    string FirstMidName,
    DateOnly EnrollmentDate,
    IReadOnlyList<EnrollmentResponse> Enrollments);