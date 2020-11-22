using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KareAjans.Models
{
    public class Staff : Person
    {
        public double Salary { get; set; }
        public double PerformanceScore { get; set; }
        //public ICollection<Actor> ActorsManaged { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
