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
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public Location source { get; set; }
            public Location Destination { get; set; }
            public double Distance { get; set; }
            public override string ToString()
            {
                return $"\nId:{Id}\nIn travel:{InTravel}\nWeight:{Weight}\nPriority:{Priorities}\nSender:{Sender}\n" +
                    $"Target:{Target}\nSource location:\n{source}\nDestination location:\n{Destination}\nDistance: {Distance}\n";
            }
        }
    }
}
