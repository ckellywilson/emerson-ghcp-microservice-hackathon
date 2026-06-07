using Microsoft.EntityFrameworkCore;

namespace StudentService.Infrastructure.Data;

public sealed class StudentServiceDbContext : DbContext
{
    public StudentServiceDbContext(DbContextOptions<StudentServiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<StudentEntity> Students => Set<StudentEntity>();

    public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>(entity =>
        {
            entity.ToTable("Students");
            entity.HasKey(item => item.StudentId);
            entity.Property(item => item.LastName).HasMaxLength(100).IsRequired();
            entity.Property(item => item.FirstMidName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<EnrollmentEntity>(entity =>
        {
            entity.ToTable("Enrollments");
            entity.HasKey(item => item.EnrollmentId);
            entity.Property(item => item.CourseTitle).HasMaxLength(200).IsRequired();
            entity.Property(item => item.Status).HasMaxLength(50).IsRequired();
            entity.Property(item => item.ProjectionStatus).HasMaxLength(50).IsRequired();
            entity.HasOne<StudentEntity>()
                .WithMany()
                .HasForeignKey(item => item.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}