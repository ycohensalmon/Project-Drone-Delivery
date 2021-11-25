using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public Int32 Id { get; set; }
            public string Name { get; set; }
            public Int32 Phone { get; set; }
            public Location Location { get; set; }
            public List<ParcelAtCustomer> ParcelsFromCustomer { get; set; } = new();
            public List<ParcelAtCustomer> ParcelsToCustomer { get; set; } = new();
            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, Location:{Location}, ParcelsFromCustomer:{ParcelsFromCustomer}, ParcelsToCustomer:{ParcelsToCustomer}";
            }
        }
    }
}
