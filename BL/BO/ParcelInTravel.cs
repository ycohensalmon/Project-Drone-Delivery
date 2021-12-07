using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInTravel
        {
            public int Id { get; set; }
            public bool InTravel { get; set; }
            public WeightCategory Weight { get; set; }
            public Priority Priorities { get; set; }
            public CustomerInParcel Target { get; set; }
            public CustomerInParcel Sender { get; set; }
            public Location source { get; set; }
            public Location Destination { get; set; }
            public int Distance { get; set; }
        }
    }
}
