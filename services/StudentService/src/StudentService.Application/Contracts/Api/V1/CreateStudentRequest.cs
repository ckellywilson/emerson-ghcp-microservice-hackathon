namespace StudentService.Application.Contracts.Api.V1;

public sealed record CreateStudentRequest(
    string LastName,
    string FirstMidName,
    DateOnly EnrollmentDate);