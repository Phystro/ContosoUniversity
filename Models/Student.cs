using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models;

public class Student
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength=2)]
    [Display(Name = "Last Name")]
    public required string LastName { get; set; } = default!;

    [StringLength(50, MinimumLength=2, ErrorMessage="First name cannot be longer then 50 characters.")]
    [Column("FirstName")]   // map Student.FirstMidName in model to FirstName column of the Student table
    [Display(Name = "First Name")]
    public required string FirstMidName { get; set; } = default!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public required DateTime EnrollmentDate { get; set; }

    [Display(Name = "Full Name")]
    public string FullName
    {
        get
        {
            return LastName + ", " + FirstMidName;
        }
    }

    // Navigation Property
    public ICollection<Enrollment> Enrollments { get; set; } = default!;
}
