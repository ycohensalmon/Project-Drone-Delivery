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
                string from = "\n", to = "\n";
                int i = 1;
                foreach (var item in ParcelsFromCustomer){from += $"{i++}: {item}\n";}
                i = 1;
                foreach (var item in ParcelsToCustomer) {to += $"{i++}: {item}\n";}
                if (from == "\n")
                    from = "0";
                if (to == "\n")
                    to = "0";

                return $"Id:{Id}\nName:{Name}\nPhone:{Phone}\nLocation:\n{Location}\nParcelsFromCustomer:{from}\nParcelsToCustomer:{to}\n--------------";
            }
        }
    }
}
