using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students;

public class DeleteModel : PageModel
{
    private readonly SchoolContext _context;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ILogger<DeleteModel> logger, SchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; } = null!;
    public string ErrorMessage { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
    {
        if (id == null)
        {
            return NotFound();
        }

        Student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Student == null)
        {
            return NotFound();
        }

        if (saveChangesError.GetValueOrDefault())
        {
            ErrorMessage = String.Format("Delete {Id} failed. Try again", id);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        try
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        catch (DbUpdateException ex)
        {
            // transient network errors likely to make delete operation fail
            // saveChangesError = false when Delete page is called from UI
            // saveChangesError = true is called from OnPostAsync because delete operation
            //  failed.
            _logger.LogError(ex, ErrorMessage);
            return RedirectToAction("./Delete", new { id, saveChangesError = true } );
        }
    }
}
