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
    public class DetailsModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public DetailsModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public Actor Actor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.Actors.Include(a => a.Contracts).ThenInclude(c => c.ManagingStaff).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Actor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
