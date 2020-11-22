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
    public class DeleteModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public DeleteModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contract Contract { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveError)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contract = await _context.Contracts.Include(c => c.Actor).Include(c => c.ManagingStaff).FirstOrDefaultAsync(m => m.ID == id);

            if (Contract == null)
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

            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            try
            {
                _context.Contracts.Remove(contract);
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
