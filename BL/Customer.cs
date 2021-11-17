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
            private Int32 id;
            private string name;
            private Int32 phone;
            private Location location;
            private List<ParcelAtCustomer> parcelFromCustomer = new();
            private List<ParcelAtCustomer> parcelsToCustomer = new();

            public Int32 Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public Int32 Phone { get => phone; set => phone = value; }
            public Location Location { get => location; set => location = value; }
            public List<ParcelAtCustomer> ParcelsFromCustomer { get => parcelFromCustomer; set => parcelFromCustomer = value; }
            public List<ParcelAtCustomer> ParcelsToCustomer { get => parcelsToCustomer; set => parcelsToCustomer = value; }
            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, Location:{Location}, ParcelsFromCustomer:{ParcelsFromCustomer}, ParcelsToCustomer:{ParcelsToCustomer}";
            }
        }
    }
}
