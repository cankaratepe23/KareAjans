using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Actors
{
    public class EditModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public EditModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.Actors.FindAsync(id);

            if (Actor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var actorToUpdate = await _context.Actors.FindAsync(id);

            if (actorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Actor>(actorToUpdate, "actor", a => a.ContractPrice, a => a.FirstName, a => a.LastName, a => a.PhoneNumber, a => a.Address)) // Overposting protection
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
