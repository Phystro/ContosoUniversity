using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models;

public class Instructor
{
    public int Id { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(50, MinimumLength=2)]
    public required string LastName { get; set; }

    [Column("FirstName")]
    [Display(Name = "First Name")]
    [StringLength(50, MinimumLength=2)]
    public required string FirstMidName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name ="Hire Date")]
    public DateTime HireDate { get; set; }

    [Display(Name = "Full Name")]
    public string FullName
    {
        get { return LastName + ", " + FirstMidName; }
    }

    public ICollection<Course> Courses { get; set; }
    public OfficeAssignment? OfficeAssignment { get; set; }
}
