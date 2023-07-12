using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students;

public class EditModel : PageModel
{
    public readonly SchoolContext _context;

    public EditModel(SchoolContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; }

    #region snippet_OnGetPost
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // FindAsync is more efficient here since we don't have to include related data
        Student = await _context.Students.FindAsync(id);
        if (Student == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var studentToUpdate = await _context.Students.FindAsync(id);
        Console.WriteLine($"Student to update: {studentToUpdate.LastName}");

        if(studentToUpdate == null)
        {
            return NotFound();
        }

        Console.WriteLine(Student.LastName);

        studentToUpdate.FirstMidName = Student.FirstMidName;
        studentToUpdate.LastName = Student.LastName;
        studentToUpdate.EnrollmentDate = Student.EnrollmentDate.ToUniversalTime();

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");

        // if (await TryUpdateModelAsync<Student>(
        //             studentToUpdate,
        //             "student",
        //             s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate
        //             )
        //    )
        // {
        //     await _context.SaveChangesAsync();
        //     Console.WriteLine("Saved Changes");
        //     return RedirectToPage("./Index");
        // }

        // return Page();
    }
    #endregion

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
