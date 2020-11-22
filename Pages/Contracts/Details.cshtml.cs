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
    public class DetailsModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public DetailsModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public Contract Contract { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contract = await _context.Contracts.Include(c => c.ManagingStaff).Include(c => c.Actor).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Contract == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
