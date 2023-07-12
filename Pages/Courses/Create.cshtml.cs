using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCourse = new Course();

            emptyCourse.Title = Course.Title;
            emptyCourse.Credits = Course.Credits;
            emptyCourse.CourseId = Course.CourseId;
            emptyCourse.DepartmentId = Course.DepartmentId;

            try
            {
                _context.Courses.Add(emptyCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                // Select DepartmentID if TryUpdateModelAsync fails.
                PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentId);
                return Page();
            }
            // if (await TryUpdateModelAsync<Course>(
            //      emptyCourse,
            //      "course",   // Prefix for form value.
            //      s => s.CourseId, s => s.DepartmentId, s => s.Title, s => s.Credits))
            // {
            //     _context.Courses.Add(emptyCourse);
            //     await _context.SaveChangesAsync();
            //     return RedirectToPage("./Index");
            // }

        }
      }
}
