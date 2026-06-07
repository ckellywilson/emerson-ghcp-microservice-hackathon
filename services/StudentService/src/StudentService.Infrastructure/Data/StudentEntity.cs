namespace StudentService.Infrastructure.Data;

public sealed class StudentEntity
{
    public int StudentId { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string FirstMidName { get; set; } = string.Empty;

    public DateOnly EnrollmentDate { get; set; }
}