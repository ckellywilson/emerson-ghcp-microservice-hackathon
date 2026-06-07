using ContosoUniversity.Web.Services;
using ContosoUniversity.Web.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Web.Controllers;

[ApiController]
[Route("api/debug/student-service")]
[AllowAnonymous]
public sealed class StudentServiceBridgeController : ControllerBase
{
    private readonly IStudentServiceAclClient _studentServiceAclClient;
    private readonly ILogger<StudentServiceBridgeController> _logger;

    public StudentServiceBridgeController(IStudentServiceAclClient studentServiceAclClient, ILogger<StudentServiceBridgeController> logger)
    {
        _studentServiceAclClient = studentServiceAclClient;
        _logger = logger;
    }

    [HttpPost("students")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentServiceCreateStudentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Monolith debug bridge intercepting CreateStudent and forwarding to StudentService.");
        var result = await _studentServiceAclClient.CreateStudentAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("students/{studentId:int}/enrollments")]
    public async Task<IActionResult> CreateEnrollment(int studentId, [FromBody] StudentServiceCreateEnrollmentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Monolith debug bridge intercepting CreateEnrollment and forwarding to StudentService.");
        var result = await _studentServiceAclClient.CreateEnrollmentAsync(studentId, request, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("students/{studentId:int}/enrollments/{enrollmentId:int}/grade")]
    public async Task<IActionResult> UpdateEnrollmentGrade(int studentId, int enrollmentId, [FromBody] StudentServiceUpdateEnrollmentGradeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Monolith debug bridge intercepting UpdateEnrollmentGrade and forwarding to StudentService.");
        var result = await _studentServiceAclClient.UpdateEnrollmentGradeAsync(studentId, enrollmentId, request, cancellationToken);
        return Ok(result);
    }
}
