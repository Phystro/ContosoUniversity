using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;
    private readonly IConfiguration _configuration;

    public IndexModel(SchoolContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public PaginatedList<Student> Students { get; set; }

    public async Task OnGetAsync(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageIndex
            )
    {
        CurrentSort = sortOrder;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        DateSort = sortOrder == "Date" ? "date_desc" : "Date";

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        //no query is sent to database. Query isn't executed until IQueryable object
        //is converted into a collection e.g. by calling a method such as ToListAsync.
        IQueryable<Student> studentsIq = from s in _context.Students select s;

        if (!String.IsNullOrEmpty(searchString))
        {
            studentsIq = studentsIq.Where(s => 
                    s.LastName.Contains(searchString) ||
                    s.FirstMidName.Contains(searchString)
                    );
        }

        switch (sortOrder)
        {
            case "name_desc":
                studentsIq = studentsIq.OrderByDescending(s => s.LastName);
                break;
            case "Date":
                studentsIq = studentsIq.OrderBy(s => s.EnrollmentDate);
                break;
            case "date_desc":
                studentsIq = studentsIq.OrderByDescending(s => s.EnrollmentDate);
                break;
            default:
                studentsIq = studentsIq.OrderBy(s => s.LastName);
                break;
        }

        var pageSize = _configuration.GetValue("PageSize", 4);
        Students = await PaginatedList<Student>.CreateAsync(
                studentsIq.AsNoTracking(), pageIndex ?? 1, pageSize
                );
        // Console.WriteLine($"PageIndex: {Students.PageIndex} and PageSize: {Students.TotalPages}");

        // pageIndex = Students[0];
        // pageSize = Students[3];
    }
}
