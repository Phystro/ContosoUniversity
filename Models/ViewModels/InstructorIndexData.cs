namespace ContosoUniversity.Models.ViewModels;

public class InstructorIndexData
{
    public IEnumerable<Instructor> Instructors { get; set; } = default!;
    public IEnumerable<Course> Courses { get; set; } = default!;
    public IEnumerable<Enrollment> Enrollments { get; set; } = default!;
}
