using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            public Int32 Id { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public WeightCategory Weight { get; set; }
            public Priority Priorities { get; set; }
            public CustomerInParcel CustomerInParcel { get; set; }

            public override string ToString()
            {
                string sp = "   ";
                return $" Id: {Id}\n{sp}Requested: {Requested}\n{sp}Scheduled: {Scheduled}\n{sp}PickedUp: {PickedUp}\n{sp}Delivered: {Delivered}\n{sp}Weight: {Weight}\n{sp}Priorities:{Priorities}\n{sp}CustomerInParcel:\n";
            }
        }
    }
}
