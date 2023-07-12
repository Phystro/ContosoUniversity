using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    // enable ef core logging for easire debugging
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(
                    action: Console.WriteLine,
                    minimumLevel: LogLevel.Information
                  )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<OfficeAssignment> OfficeAssignments => Set<OfficeAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable(nameof(Courses))
            .HasMany(c => c.Instructors)
            .WithMany(i => i.Courses);
        // modelBuilder.Entity<Enrollment>().ToTable(nameof(Enrollments));
        modelBuilder.Entity<Student>().ToTable(nameof(Students));
        modelBuilder.Entity<Instructor>().ToTable(nameof(Instructors));
    }
}
