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
            private Int32 id;
            private bool inTravel;
            private Priority priorities;
            private WeightCategory weight;
            private CustomerInParcel target;
            private CustomerInParcel sender;
            private Location departure;
            private Location destination;
            private int distance;

            public int Id { get => id; set => id = value; }
            public bool InTravel { get => inTravel; set => inTravel = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public CustomerInParcel Target { get => target; set => target = value; }
            public CustomerInParcel Sender { get => sender; set => sender = value; }
            public Location Departure { get => departure; set => departure = value; }
            public Location Destination { get => destination; set => destination = value; }
            public int Distance { get => distance; set => distance = value; }
        }
    }
}
