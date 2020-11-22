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
    public class DeleteModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public DeleteModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveError)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.Actors.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Actor == null)
            {
                return NotFound();
            }
            if (saveError.GetValueOrDefault())
            {
                ErrorMessage = "Could not delete the selected record. Please try again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            try
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("./Delete", new { id, saveError = true });
            }
        }
    }
}
