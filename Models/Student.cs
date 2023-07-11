namespace ContosoUniversity.Models;

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; } = default!;
    public string FirstMidName { get; set; } = default!;
    public DateTime EnrollmentDate { get; set; }

    // Navigation Property
    public ICollection<Enrollment> Enrollments { get; set; } = default!;
}
