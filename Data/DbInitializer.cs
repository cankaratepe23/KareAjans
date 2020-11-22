using KareAjans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KareAjans.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AgencyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Actors.Any())
            {
                return; // DB has data
            }

            var actors = new Actor[]
            {
                new Actor{ FirstName="John", LastName="Smith", Address="74 Hendford Hill", ContractPrice=100, PhoneNumber="+15555555555" },
                new Actor{ FirstName="Leo", LastName="Gardener", Address="55 Glandovey Terrace", ContractPrice=100, PhoneNumber="+15554864523" },
                new Actor{ FirstName="Eloise", LastName="Stephenson", Address="83 Merthyr Road", ContractPrice=100, PhoneNumber="+15486487564" },
                new Actor{ FirstName="Lucy", LastName="Stanley", Address="39 Hendford Hill", ContractPrice=100, PhoneNumber="+15486548762" },
                new Actor{ FirstName="Eleanor", LastName="Cole", Address="49 Caxton Palace", ContractPrice=100, PhoneNumber="+15432156489" },
                new Actor{ FirstName="Toby", LastName="James", Address="52 Sea Road", ContractPrice=100, PhoneNumber="+15642234598" },
                new Actor{ FirstName="Rebecca", LastName="Blackburn", Address="41 Constitution St", ContractPrice=100, PhoneNumber="+12459846523" }
            };
            context.Actors.AddRange(actors);
            context.SaveChanges();

            var staff = new Staff[]
            {
                new Staff{ FirstName="Finley", LastName="Marsden", Address="46 Old Edinburgh Road", PhoneNumber="+12544654865", PerformanceScore=3.2, Salary=5000 },
                new Staff{ FirstName="Evie", LastName="Pope", Address="89 Park End St", PhoneNumber="+14586542155", PerformanceScore=2.7, Salary=4700 }
            };
            context.Staff.AddRange(staff);
            context.SaveChanges();

            var contracts = new Contract[]
            {
                new Contract{ Actor=actors.First(), ManagingStaff=staff.First(), StartDate=new DateTime(2020, 11, 20), EndDate=new DateTime(2020, 11, 27), TotalPrice=10000  },
                new Contract{ Actor=actors.First(), ManagingStaff=staff[1], StartDate=new DateTime(2018, 06, 17), EndDate=new DateTime(2018, 9, 7), TotalPrice=150000  },
                new Contract{ Actor=actors.First(), ManagingStaff=staff.First(), StartDate=new DateTime(2019, 03, 20), EndDate=new DateTime(2019, 03, 20), TotalPrice=50000  },
                new Contract{ Actor=actors.First(), ManagingStaff=staff.First(), StartDate=new DateTime(2020, 08, 13), EndDate=new DateTime(2020, 08, 14), TotalPrice=1100  },
                new Contract{ Actor=actors[1], ManagingStaff=staff[1], StartDate=new DateTime(2005, 01, 05), EndDate=new DateTime(2005, 01, 15), TotalPrice=7800  },
                new Contract{ Actor=actors[1], ManagingStaff=staff[1], StartDate=new DateTime(2010, 02, 18), EndDate=new DateTime(2010, 02, 25), TotalPrice=8400  },
                new Contract{ Actor=actors[1], ManagingStaff=staff[1], StartDate=new DateTime(2012, 09, 23), EndDate=new DateTime(2012, 10, 05), TotalPrice=8500  },
                new Contract{ Actor=actors[1], ManagingStaff=staff[1], StartDate=new DateTime(2018, 07, 14), EndDate=new DateTime(2018, 09, 27), TotalPrice=12000  }
            };
            context.Contracts.AddRange(contracts);
            context.SaveChanges();
        }
    }
}
