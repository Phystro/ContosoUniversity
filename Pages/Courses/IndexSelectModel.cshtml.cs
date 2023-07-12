using ContosoUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexSelectModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexSelectModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        #region snippet_RevisedIndexMethod
        public IList<CourseViewModel> CourseVM { get; set; }

        public async Task OnGetAsync()
        {
            CourseVM = await _context.Courses
            .Select(p => new CourseViewModel
            {
                CourseId = p.CourseId,
                Title = p.Title,
                Credits = p.Credits,
                DepartmentName = p.Department.Name
            }).ToListAsync();
        }
        #endregion
    }
}
