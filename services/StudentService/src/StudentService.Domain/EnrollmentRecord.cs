namespace StudentService.Domain;

public sealed class EnrollmentRecord
{
    public int EnrollmentId { get; init; }

    public int StudentId { get; init; }

    public int CourseId { get; init; }

    public string CourseTitle { get; set; } = string.Empty;

    public string? Grade { get; set; }

    public string Status { get; set; } = "Active";

    public string ProjectionStatus { get; set; } = "Current";
}