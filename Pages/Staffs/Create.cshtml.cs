using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Staffs
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
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyStaff = new Staff();
            if (await TryUpdateModelAsync<Staff>(emptyStaff, "staff", s => s.Salary, s => s.PerformanceScore, s => s.FirstName, s => s.LastName, s => s.PhoneNumber, s => s.Address)) // Overposting protection, only update the listed fields.
            {
                _context.Staff.Add(emptyStaff);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
