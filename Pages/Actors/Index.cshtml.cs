using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Actors
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

        public IList<Actor> Actor { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            IQueryable<Actor> actorsIQ = _context.Actors.Select(a => a);
            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                // For performance reasons, the search is done solely on the SQL DB side. For improved UX this could be changed to search in a.FullName instead, but that would most likely require the query to be executed in-memory.
                actorsIQ = actorsIQ.Where(a => a.FirstName.Contains(searchString) || a.LastName.Contains(searchString));
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
                actorsIQ = actorsIQ.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                actorsIQ = actorsIQ.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
            Actor = await actorsIQ.AsNoTracking().ToListAsync();
        }
    }
}
