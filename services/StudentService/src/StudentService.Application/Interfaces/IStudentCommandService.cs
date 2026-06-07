using StudentService.Application.Contracts.Api.V1;

namespace StudentService.Application.Interfaces;

public interface IStudentCommandService
{
    Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken);

    Task<EnrollmentResponse?> CreateEnrollmentAsync(int studentId, CreateEnrollmentRequest request, CancellationToken cancellationToken);

    Task<EnrollmentResponse?> UpdateEnrollmentGradeAsync(int studentId, int enrollmentId, UpdateEnrollmentGradeRequest request, CancellationToken cancellationToken);
}