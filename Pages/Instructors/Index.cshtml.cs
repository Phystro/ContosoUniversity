#region snippet_all
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.ViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public async Task OnGetAsync(int? id, int? courseId)
        {
            #region snippet_query
            InstructorData = new InstructorIndexData();
            InstructorData.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                    .ThenInclude(c => c.Department)
                .OrderBy(i => i.LastName)
                .ToListAsync();
            #endregion

            #region snippet_id
            // when instructor is selected
            if (id != null)
            {
                InstructorId = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.Id == id.Value).Single();
                InstructorData.Courses = instructor.Courses;
            }
            #endregion

            #region snippet_enrollment
            if (courseId != null)
            {
                CourseId = courseId.Value;
                IEnumerable<Enrollment> Enrollments = await _context.Enrollments
                    .Where(x => x.CourseId == CourseId)
                    .Include(i=>i.Student)
                    .ToListAsync();
                InstructorData.Enrollments = Enrollments;
            }
            #endregion
        }
    }
}
#endregion
