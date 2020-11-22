using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KareAjans.Models
{
    public class Contract
    {
        public int ID { get; set; }
        public int ActorID { get; set; } // Explicit FK property for making Create/Update code simpler
        public Actor Actor { get; set; }
        public int ManagingStaffID { get; set; } // Explicit FK property for making Create/Update code simpler
        public Staff ManagingStaff { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
