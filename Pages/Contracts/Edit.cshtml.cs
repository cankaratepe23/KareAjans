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

namespace KareAjans.Pages.Contracts
{
    public class EditModel : ContractsRelationsPageModelModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public EditModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contract Contract { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            PopulateDropDownList(_context, Contract.ManagingStaffID, Contract.ActorID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractToUpdate = await _context.Contracts.FindAsync(id);

            if (contractToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Contract>(contractToUpdate, "contract", c => c.StartDate, c => c.EndDate, c => c.TotalPrice, c => c.ActorID, c => c.ManagingStaffID)) // Overposting protection, only update the listed fields.
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateDropDownList(_context, contractToUpdate.ManagingStaffID, contractToUpdate.ActorID);
            return Page();
        }
    }
}
