using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Actors
{
    public class CreateModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public CreateModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Actor Actor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyActor = new Actor();
            if (await TryUpdateModelAsync<Actor>(emptyActor, "actor", a => a.ContractPrice, a => a.FirstName, a => a.LastName, a => a.PhoneNumber, a => a.Address)) // Overposting protection, only update the listed fields.
            {
                _context.Actors.Add(emptyActor);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
