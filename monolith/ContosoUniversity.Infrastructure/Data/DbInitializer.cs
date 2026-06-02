using ContosoUniversity.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(SchoolContext context, ILogger logger)
        {
            try
            {
                // Ensure the database is created
                context.Database.EnsureCreated();

                // Look for any students - if found, the DB has been seeded
                if (await context.Students.AnyAsync())
                {
                    logger.LogInformation("Database already contains data - skipping seeding");
                    return;
                }

                logger.LogInformation("Starting database seeding...");

                var students = new Student[]
                {
                    new Student { FirstMidName = "Carson",   LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2023-09-01") },
                    new Student { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2023-09-01") },
                    new Student { FirstMidName = "Arturo",   LastName = "Anand",
                        EnrollmentDate = DateTime.Parse("2024-01-15") },
                    new Student { FirstMidName = "Yan",      LastName = "Li",
                        EnrollmentDate = DateTime.Parse("2024-09-01") }
                };

                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} students", students.Length);

                var instructors = new Instructor[]
                {
                    new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                        HireDate = DateTime.Parse("2014-08-15") },
                    new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                        HireDate = DateTime.Parse("2016-07-06") },
                    new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                        HireDate = DateTime.Parse("2015-01-12") }
                };

                await context.Instructors.AddRangeAsync(instructors);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} instructors", instructors.Length);

                var departments = new Department[]
                {
                    new Department { Name = "English",     Budget = 150000,
                        StartDate = DateTime.Parse("2020-09-01"),
                        InstructorID  = instructors.Single(i => i.LastName == "Abercrombie").ID },
                    new Department { Name = "Mathematics", Budget = 175000,
                        StartDate = DateTime.Parse("2020-09-01"),
                        InstructorID  = instructors.Single(i => i.LastName == "Fakhouri").ID },
                    new Department { Name = "Engineering", Budget = 200000,
                        StartDate = DateTime.Parse("2020-09-01"),
                        InstructorID  = instructors.Single(i => i.LastName == "Harui").ID }
                };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} departments", departments.Length);

                var courses = new Course[]
                {
                    new Course {CourseID = 1050, Title = "Chemistry", Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "Engineering").DepartmentID
                    },
                    new Course {CourseID = 1045, Title = "Calculus", Credits = 4,
                        DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID
                    },
                    new Course {CourseID = 2021, Title = "Composition", Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "English").DepartmentID
                    },
                };

                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} courses", courses.Length);

                var officeAssignments = new OfficeAssignment[]
                {
                    new OfficeAssignment {
                        InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID,
                        Location = "Smith 17" },
                    new OfficeAssignment {
                        InstructorID = instructors.Single(i => i.LastName == "Harui").ID,
                        Location = "Gowan 27" },
                };

                await context.OfficeAssignments.AddRangeAsync(officeAssignments);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} office assignments", officeAssignments.Length);

                var courseInstructors = new CourseAssignment[]
                {
                    new CourseAssignment {
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                        InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                    new CourseAssignment {
                        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                        InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                    new CourseAssignment {
                        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                        InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                };

                await context.CourseAssignments.AddRangeAsync(courseInstructors);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} course assignments", courseInstructors.Length);

                var enrollments = new Enrollment[]
                {
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                        Grade = Grade.A
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alexander").ID,
                        CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                        Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                        Grade = Grade.A
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                        Grade = Grade.B
                    },
                    new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Anand").ID,
                        CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                        Grade = Grade.B
                    }
                };

                await context.Enrollments.AddRangeAsync(enrollments);
                await context.SaveChangesAsync();
                logger.LogInformation("Added {count} enrollments", enrollments.Length);

                logger.LogInformation("Database seeding completed successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}
