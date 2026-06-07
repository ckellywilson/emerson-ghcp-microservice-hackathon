namespace StudentService.Domain;

public sealed class StudentAggregate
{
    public int StudentId { get; init; }

    public string LastName { get; init; } = string.Empty;

    public string FirstMidName { get; init; } = string.Empty;

    public DateOnly EnrollmentDate { get; init; }

    public List<EnrollmentRecord> Enrollments { get; } = new();
}