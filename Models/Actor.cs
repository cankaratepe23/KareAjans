using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KareAjans.Models
{
    public class Actor : Person
    {
        public Staff Manager { get; set; }
        public double ContractPrice { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
