using StudentService.Application.Contracts.Api.V1;
using StudentService.Application.Interfaces;
using StudentService.BoundaryAdapters.Monolith;
using StudentService.Infrastructure;
using StudentService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStudentInfrastructure(builder.Configuration);
builder.Services.AddMonolithBoundaryAdapters();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<StudentServiceDbContext>();
	dbContext.Database.EnsureCreated();
}

app.MapGet("/", () => Results.Ok(new
{
	service = "StudentService",
	version = "v1",
	purpose = "Student and Enrollment service shell for Day-2 extraction"
}));

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

var students = app.MapGroup("/api/v1/students");

students.MapGet("/", async (IStudentQueryService queryService, CancellationToken cancellationToken) =>
{
	var allStudents = await queryService.GetStudentsAsync(cancellationToken);
	return Results.Ok(allStudents);
});

students.MapPost("/", async (CreateStudentRequest request, IStudentCommandService commandService, CancellationToken cancellationToken) =>
{
	var student = await commandService.CreateStudentAsync(request, cancellationToken);
	return Results.Created($"/api/v1/students/{student.StudentId}", student);
});

students.MapGet("/{studentId:int}", async (int studentId, IStudentQueryService queryService, CancellationToken cancellationToken) =>
{
	var student = await queryService.GetStudentAsync(studentId, cancellationToken);
	return student is null
		? Results.NotFound(new ErrorResponse("student_not_found", "Student was not found."))
		: Results.Ok(student);
});

students.MapGet("/{studentId:int}/enrollments", async (int studentId, IStudentQueryService queryService, CancellationToken cancellationToken) =>
{
	var enrollments = await queryService.GetEnrollmentsAsync(studentId, cancellationToken);
	return enrollments is null
		? Results.NotFound(new ErrorResponse("student_not_found", "Student was not found."))
		: Results.Ok(enrollments);
});

students.MapPost("/{studentId:int}/enrollments", async (int studentId, CreateEnrollmentRequest request, IStudentCommandService commandService, CancellationToken cancellationToken) =>
{
	var enrollment = await commandService.CreateEnrollmentAsync(studentId, request, cancellationToken);
	return enrollment is null
		? Results.NotFound(new ErrorResponse("student_not_found", "Student was not found."))
		: Results.Created($"/api/v1/students/{studentId}/enrollments/{enrollment.EnrollmentId}", enrollment);
});

students.MapPatch("/{studentId:int}/enrollments/{enrollmentId:int}/grade", async (int studentId, int enrollmentId, UpdateEnrollmentGradeRequest request, IStudentCommandService commandService, CancellationToken cancellationToken) =>
{
	var enrollment = await commandService.UpdateEnrollmentGradeAsync(studentId, enrollmentId, request, cancellationToken);
	return enrollment is null
		? Results.NotFound(new ErrorResponse("enrollment_not_found", "Enrollment was not found."))
		: Results.Ok(enrollment);
});

app.Run();
