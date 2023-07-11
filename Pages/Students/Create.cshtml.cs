using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students;

public class CreateModel : PageModel
{
    private readonly SchoolContext _context;

    public CreateModel(SchoolContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        Student = new Student { EnrollmentDate = DateTime.Now, FirstMidName = "Joe", LastName = "Smith" };
        return Page();
    }

    [BindProperty]
    public Student Student { get; set; }

    #region snippet_OnPostAsync
    public async Task<IActionResult> OnPostAsync()
    {
    #region snippet_TryUpdateModelAsync
        var emptyStudent = new Student()
        {
            FirstMidName = Student.FirstMidName,
            LastName = Student.LastName,
            EnrollmentDate = Student.EnrollmentDate.ToUniversalTime()
        };

        _context.Students.Add(emptyStudent);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");

        // if(await TryUpdateModelAsync<Student>(
        //             emptyStudent,
        //             "student", // prefix for form value
        //             s => s.FirstMidName,
        //             s => s.LastName,
        //             s => s.EnrollmentDate))
        // {
        //     _context.Students.Add(emptyStudent);
        //     await _context.SaveChangesAsync();
        //     return RedirectToPage("./Index");
        // }
        #endregion

        // return Page();
    }
    #endregion
}
