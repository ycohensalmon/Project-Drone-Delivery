using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelAtCustomer
    {
        public Int32 Id { get; set; }
        public ParcelStatuses Status { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priorities { get; set; }
        public CustomerInParcel CustomerInParcel { get; set; }

        public override string ToString()
        {
            string sp = "   ";
            return $"Id: {Id}\n{sp}Weight: {Weight}\n{sp}Priorities:{Priorities}\n{sp}CustomerInParcel:\n{CustomerInParcel}";
        }
    }
}
