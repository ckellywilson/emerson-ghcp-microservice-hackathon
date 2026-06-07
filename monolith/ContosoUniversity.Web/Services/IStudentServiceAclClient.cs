using ContosoUniversity.Web.Services.Models;

namespace ContosoUniversity.Web.Services;

public interface IStudentServiceAclClient
{
    Task<IReadOnlyList<StudentServiceStudentResponse>> GetStudentsAsync(CancellationToken cancellationToken);

    Task<StudentServiceStudentResponse> CreateStudentAsync(StudentServiceCreateStudentRequest request, CancellationToken cancellationToken);

    Task<StudentServiceEnrollmentResponse> CreateEnrollmentAsync(int studentId, StudentServiceCreateEnrollmentRequest request, CancellationToken cancellationToken);

    Task<StudentServiceEnrollmentResponse> UpdateEnrollmentGradeAsync(int studentId, int enrollmentId, StudentServiceUpdateEnrollmentGradeRequest request, CancellationToken cancellationToken);
}
