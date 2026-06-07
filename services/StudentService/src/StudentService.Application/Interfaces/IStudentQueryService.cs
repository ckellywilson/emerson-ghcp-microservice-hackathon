using StudentService.Application.Contracts.Api.V1;

namespace StudentService.Application.Interfaces;

public interface IStudentQueryService
{
    Task<IReadOnlyList<StudentResponse>> GetStudentsAsync(CancellationToken cancellationToken);

    Task<StudentResponse?> GetStudentAsync(int studentId, CancellationToken cancellationToken);

    Task<IReadOnlyList<EnrollmentResponse>?> GetEnrollmentsAsync(int studentId, CancellationToken cancellationToken);
}