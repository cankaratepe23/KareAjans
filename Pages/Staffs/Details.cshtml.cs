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
    public class DetailsModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public DetailsModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staff.Include(s => s.Contracts).ThenInclude(c => c.Actor).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Staff == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
