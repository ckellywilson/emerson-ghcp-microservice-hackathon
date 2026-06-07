using Microsoft.EntityFrameworkCore;
using StudentService.Application.Contracts.Api.V1;
using StudentService.Application.Interfaces;
using StudentService.Infrastructure.Data;

namespace StudentService.Infrastructure.Services;

public sealed class SqliteStudentService : IStudentCommandService, IStudentQueryService
{
    private readonly StudentServiceDbContext _dbContext;

    public SqliteStudentService(StudentServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = new StudentEntity
        {
            LastName = request.LastName,
            FirstMidName = request.FirstMidName,
            EnrollmentDate = request.EnrollmentDate
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapStudent(student, new List<EnrollmentEntity>());
    }

    public async Task<StudentResponse?> GetStudentAsync(int studentId, CancellationToken cancellationToken)
    {
        var student = await _dbContext.Students
            .AsNoTracking()
            .SingleOrDefaultAsync(item => item.StudentId == studentId, cancellationToken);

        if (student is null)
        {
            return null;
        }

        var enrollments = await _dbContext.Enrollments
            .AsNoTracking()
            .Where(item => item.StudentId == studentId)
            .OrderBy(item => item.EnrollmentId)
            .ToListAsync(cancellationToken);

        return MapStudent(student, enrollments);
    }

    public async Task<IReadOnlyList<StudentResponse>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        var students = await _dbContext.Students
            .AsNoTracking()
            .OrderBy(item => item.StudentId)
            .ToListAsync(cancellationToken);

        if (students.Count == 0)
        {
            return Array.Empty<StudentResponse>();
        }

        var studentIds = students.Select(item => item.StudentId).ToList();
        var enrollments = await _dbContext.Enrollments
            .AsNoTracking()
            .Where(item => studentIds.Contains(item.StudentId))
            .OrderBy(item => item.EnrollmentId)
            .ToListAsync(cancellationToken);

        var byStudent = enrollments
            .GroupBy(item => item.StudentId)
            .ToDictionary(group => group.Key, group => (IReadOnlyList<EnrollmentEntity>)group.ToList());

        return students
            .Select(student => MapStudent(student, byStudent.GetValueOrDefault(student.StudentId, Array.Empty<EnrollmentEntity>())))
            .ToList();
    }

    public async Task<IReadOnlyList<EnrollmentResponse>?> GetEnrollmentsAsync(int studentId, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Students
            .AsNoTracking()
            .AnyAsync(item => item.StudentId == studentId, cancellationToken);

        if (!exists)
        {
            return null;
        }

        var entities = await _dbContext.Enrollments
            .AsNoTracking()
            .Where(item => item.StudentId == studentId)
            .OrderBy(item => item.EnrollmentId)
            .ToListAsync(cancellationToken);

        return entities.Select(MapEnrollment).ToList();
    }

    public async Task<EnrollmentResponse?> CreateEnrollmentAsync(int studentId, CreateEnrollmentRequest request, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Students
            .AsNoTracking()
            .AnyAsync(item => item.StudentId == studentId, cancellationToken);

        if (!exists)
        {
            return null;
        }

        var enrollment = new EnrollmentEntity
        {
            StudentId = studentId,
            CourseId = request.CourseId,
            CourseTitle = $"Course {request.CourseId}",
            ProjectionStatus = "StubbedProjection",
            Status = "Active"
        };

        _dbContext.Enrollments.Add(enrollment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapEnrollment(enrollment);
    }

    public async Task<EnrollmentResponse?> UpdateEnrollmentGradeAsync(int studentId, int enrollmentId, UpdateEnrollmentGradeRequest request, CancellationToken cancellationToken)
    {
        var enrollment = await _dbContext.Enrollments
            .SingleOrDefaultAsync(item => item.StudentId == studentId && item.EnrollmentId == enrollmentId, cancellationToken);

        if (enrollment is null)
        {
            return null;
        }

        enrollment.Grade = request.Grade;
        enrollment.Status = "Completed";
        await _dbContext.SaveChangesAsync(cancellationToken);
        return MapEnrollment(enrollment);
    }

    private static StudentResponse MapStudent(StudentEntity student, IReadOnlyList<EnrollmentEntity> enrollments)
    {
        return new StudentResponse(
            student.StudentId,
            student.LastName,
            student.FirstMidName,
            student.EnrollmentDate,
            enrollments.Select(MapEnrollment).ToList());
    }

    private static EnrollmentResponse MapEnrollment(EnrollmentEntity enrollment)
    {
        return new EnrollmentResponse(
            enrollment.EnrollmentId,
            enrollment.StudentId,
            enrollment.CourseId,
            enrollment.CourseTitle,
            enrollment.Grade,
            enrollment.Status,
            enrollment.ProjectionStatus);
    }
}