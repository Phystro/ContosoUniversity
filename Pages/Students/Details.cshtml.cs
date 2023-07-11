using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students;

public class DetailsModel : PageModel
{
    private readonly SchoolContext _context;

    public DetailsModel(SchoolContext context)
    {
        _context = context;
    }

    public Student Student { get; set; }

    #region snippet_OnGetAsync
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Student = await _context.Students
            .Include(s => s.Enrollments)    // load Students.Enrollments navigation property
            .ThenInclude(e => e.Course)     // load Enrollment.Course navigation property within the Enrollment
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Student == null)
        {
            return NotFound();
        }

        return Page();
    }
    #endregion
}
