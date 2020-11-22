using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KareAjans.Data;
using KareAjans.Models;

namespace KareAjans.Pages.Contracts
{
    public class CreateModel : ContractsRelationsPageModelModel
    {
        private readonly KareAjans.Data.AgencyContext _context;

        public CreateModel(KareAjans.Data.AgencyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Contract Contract { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyContract = new Contract();
            if (await TryUpdateModelAsync<Contract>(emptyContract, "contract", c => c.StartDate, c => c.EndDate, c => c.TotalPrice, c => c.ActorID, c => c.ManagingStaffID)) // Overposting protection, only update the listed fields.
            {
                _context.Contracts.Add(emptyContract);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateDropDownList(_context, emptyContract.ManagingStaffID, emptyContract.ActorID);
            return Page();
        }
    }
}
