using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string SafePassword { get; set; }
        public Int32 Phone { get; set; }
        public string Photo { get; set; }
        public List<ParcelAtCustomer> ParcelsFromUser { get; set; } = new();
        public List<ParcelAtCustomer> ParcelsToUser { get; set; } = new();
        public override string ToString()
        {
            string from = "\n", to = "\n";
            int i = 1;
            foreach (var item in ParcelsFromUser) { from += $"{i++}: {item}\n"; }
            i = 1;
            foreach (var item in ParcelsToUser) { to += $"{i++}: {item}\n"; }
            if (from == "\n")
                from = "0";
            if (to == "\n")
                to = "0";

            return $"Id:{Id}\nName:{Name}\nPhone:{Phone}\nParcelsFromCustomer:{from}\nParcelsToCustomer:{to}\n--------------";
        }
    }
}
