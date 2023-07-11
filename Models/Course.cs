using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models;

public class Course
{
    // attribute allows app to specifythe primary key rather than having the database generate it
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseId { get; set; }
    public string Title { get; set; } = default!;
    public int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = default!;
}
