using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [StringLength(50, MinimumLength=3)]
    public string Name { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]    // changes SQL data type mapping
    public decimal Budget { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    public int? InstructorId { get; set; }

    [Timestamp]// TimestampAttribute identifies column as a concurrency tracking column
    public byte[] ConcurrencyToken { get; set; }

    public Instructor Administrator { get; set; }
    public ICollection<Course> Courses { get; set; } = default!;
}
