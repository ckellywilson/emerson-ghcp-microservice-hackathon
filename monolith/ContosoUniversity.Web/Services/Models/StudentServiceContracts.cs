namespace ContosoUniversity.Web.Services.Models;

public sealed record StudentServiceCreateStudentRequest(
    string LastName,
    string FirstMidName,
    DateOnly EnrollmentDate);

public sealed record StudentServiceCreateEnrollmentRequest(int CourseId);

public sealed record StudentServiceUpdateEnrollmentGradeRequest(string Grade);

public sealed record StudentServiceEnrollmentResponse(
    int EnrollmentId,
    int StudentId,
    int CourseId,
    string CourseTitle,
    string? Grade,
    string Status,
    string ProjectionStatus);

public sealed record StudentServiceStudentResponse(
    int StudentId,
    string LastName,
    string FirstMidName,
    DateOnly EnrollmentDate,
    IReadOnlyList<StudentServiceEnrollmentResponse> Enrollments);
