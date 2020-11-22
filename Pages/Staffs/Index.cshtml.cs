using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Staffs
{
    public class IndexModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public IndexModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Staff> Staff { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            IQueryable<Staff> staffsIQ = _context.Staff.Select(s => s);
            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                // For performance reasons, the search is done solely on the SQL DB side. For improved UX this could be changed to search in a.FullName instead, but that would most likely require the query to be executed in-memory.
                staffsIQ = staffsIQ.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "ID";
            }
            CurrentSort = sortOrder;
            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                descending = true;
                sortOrder = sortOrder.Substring(0, sortOrder.Length - ("_desc".Length));
            }

            if (descending)
            {
                staffsIQ = staffsIQ.OrderByDescending(s => EF.Property<object>(s, sortOrder));
            }
            else
            {
                staffsIQ = staffsIQ.OrderBy(s => EF.Property<object>(s, sortOrder));
            }
            Staff = await staffsIQ.AsNoTracking().ToListAsync();
        }
    }
}
