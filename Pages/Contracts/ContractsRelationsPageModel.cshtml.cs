using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KareAjans.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KareAjans.Pages.Contracts
{
    public class ContractsRelationsPageModelModel : PageModel
    {
        public SelectList ManagingStaffSL { get; set; }
        public SelectList ActorSL { get; set; }

        public void PopulateDropDownList(AgencyContext _context, object selectedStaff = null, object selectedActor = null)
        {
            var staffIQ = _context.Staff.OrderBy(s => s.FirstName);
            var actorIQ = _context.Actors.OrderBy(a => a.FirstName);

            ManagingStaffSL = new SelectList(staffIQ.AsNoTracking(), "ID", "FullName", selectedStaff);
            ActorSL = new SelectList(actorIQ.AsNoTracking(), "ID", "FullName", selectedActor);
        }
    }
}
