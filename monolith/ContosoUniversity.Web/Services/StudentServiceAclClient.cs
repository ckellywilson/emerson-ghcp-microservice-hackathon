using System.Net.Http.Json;
using ContosoUniversity.Web.Services.Models;

namespace ContosoUniversity.Web.Services;

public sealed class StudentServiceAclClient : IStudentServiceAclClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<StudentServiceAclClient> _logger;

    public StudentServiceAclClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<StudentServiceAclClient> logger)
    {
        _logger = logger;
        var baseUrl = configuration["StudentService:BaseUrl"] ?? "http://localhost:5201";
        _httpClient = httpClientFactory.CreateClient(nameof(StudentServiceAclClient));
        _httpClient.BaseAddress = new Uri(baseUrl.TrimEnd('/'));
    }

    public async Task<IReadOnlyList<StudentServiceStudentResponse>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ACL forwarding GetStudents to StudentService at {Url}", _httpClient.BaseAddress);
        var payload = await _httpClient.GetFromJsonAsync<IReadOnlyList<StudentServiceStudentResponse>>("/api/v1/students", cancellationToken);
        return payload ?? Array.Empty<StudentServiceStudentResponse>();
    }

    public async Task<StudentServiceStudentResponse> CreateStudentAsync(StudentServiceCreateStudentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ACL forwarding CreateStudent to StudentService at {Url}", _httpClient.BaseAddress);
        var response = await _httpClient.PostAsJsonAsync("/api/v1/students", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<StudentServiceStudentResponse>(cancellationToken);
        if (payload is null)
        {
            throw new InvalidOperationException("StudentService returned an empty create-student response.");
        }

        return payload;
    }

    public async Task<StudentServiceEnrollmentResponse> CreateEnrollmentAsync(int studentId, StudentServiceCreateEnrollmentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ACL forwarding CreateEnrollment for student {StudentId} to StudentService", studentId);
        var response = await _httpClient.PostAsJsonAsync($"/api/v1/students/{studentId}/enrollments", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<StudentServiceEnrollmentResponse>(cancellationToken);
        if (payload is null)
        {
            throw new InvalidOperationException("StudentService returned an empty create-enrollment response.");
        }

        return payload;
    }

    public async Task<StudentServiceEnrollmentResponse> UpdateEnrollmentGradeAsync(int studentId, int enrollmentId, StudentServiceUpdateEnrollmentGradeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ACL forwarding UpdateEnrollmentGrade for student {StudentId}, enrollment {EnrollmentId} to StudentService", studentId, enrollmentId);
        var response = await _httpClient.PatchAsJsonAsync($"/api/v1/students/{studentId}/enrollments/{enrollmentId}/grade", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<StudentServiceEnrollmentResponse>(cancellationToken);
        if (payload is null)
        {
            throw new InvalidOperationException("StudentService returned an empty update-grade response.");
        }

        return payload;
    }
}
