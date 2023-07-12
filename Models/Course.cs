using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models;

public class Course
{
    // attribute allows app to specifythe primary key rather than having the database generate it
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Number")]
    public int CourseId { get; set; }

    [StringLength(50, MinimumLength=3)]
    public string Title { get; set; } = default!;

    [Range(0, 5)]
    public int Credits { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = default!;
    public ICollection<Instructor> Instructors { get; set; } = default!;
}
