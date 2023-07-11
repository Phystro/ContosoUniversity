using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models;

public class Enrollment
{
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    [DisplayFormat(NullDisplayText = "No grade")]
    public Grade? Grade { get; set; }

    public Course Course { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
