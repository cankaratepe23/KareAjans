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

namespace KareAjans.Pages.Staffs
{
    public class EditModel : PageModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public EditModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Staff = await _context.Staff.FindAsync(id);

            if (Staff == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var staffToUpdate = await _context.Staff.FindAsync(id);

            if (staffToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Staff>(staffToUpdate, "staff", s => s.Salary, s => s.PerformanceScore, s => s.FirstName, s => s.LastName, s => s.PhoneNumber, s => s.Address)) // Overposting protection
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.ID == id);
        }
    }
}
