using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Contracts
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

        public IList<Contract> Contract { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            IQueryable<Contract> contractsIQ = _context.Contracts.Include(c => c.Actor).Include(c => c.ManagingStaff).AsNoTracking();
            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                // For performance reasons, the search is done solely on the SQL DB side. For improved UX this could be changed to search in a.FullName instead, but that would most likely require the query to be executed in-memory.
                contractsIQ = contractsIQ.Where(c => c.Actor.FirstName.Contains(searchString) || c.Actor.LastName.Contains(searchString) || c.ManagingStaff.FirstName.Contains(searchString) || c.ManagingStaff.LastName.Contains(searchString));
            }

            if (String.IsNullOrEmpty(sortOrder))
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
                contractsIQ = contractsIQ.OrderByDescending(c => EF.Property<object>(c, sortOrder));
            }
            else
            {
                contractsIQ = contractsIQ.OrderBy(c => EF.Property<object>(c, sortOrder));
            }

            Contract = await contractsIQ.ToListAsync();
        }
    }
}
