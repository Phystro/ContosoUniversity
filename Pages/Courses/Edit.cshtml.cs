using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses
                .Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }

            // Select current DepartmentID.
            PopulateDepartmentsDropDownList(_context, Course.DepartmentId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            courseToUpdate.DepartmentId = Course.DepartmentId;
            courseToUpdate.Credits = Course.Credits;
            courseToUpdate.Title = Course.Title;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                // Select DepartmentID if TryUpdateModelAsync fails.
                PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentId);
                return Page();
            }

            // if (await TryUpdateModelAsync<Course>(
            //      courseToUpdate,
            //      "course",   // Prefix for form value.
            //        c => c.Credits, c => c.DepartmentId, c => c.Title))
            // {
            //     await _context.SaveChangesAsync();
            //     return RedirectToPage("./Index");
            // }

        }
    }
}
