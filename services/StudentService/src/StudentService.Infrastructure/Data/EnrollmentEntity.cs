namespace StudentService.Infrastructure.Data;

public sealed class EnrollmentEntity
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public string CourseTitle { get; set; } = string.Empty;

    public string? Grade { get; set; }

    public string Status { get; set; } = "Active";

    public string ProjectionStatus { get; set; } = "Current";
}