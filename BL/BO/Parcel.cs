using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            private int id;
            private CustomerInParcel sender;
            private CustomerInParcel target;
            private DroneInParcel drone;
            private DateTime requested;
            private DateTime scheduled;
            private DateTime pickedUp;
            private DateTime delivered;
            private WeightCategory weight;
            private Priority priorities;

            public int Id { get => id; set => id = value; }
            public CustomerInParcel Sender { get => sender; set => sender = value; }
            public CustomerInParcel Target { get => target; set => target = value; }
            public DroneInParcel Drone { get => drone; set => drone = value; }
            public DateTime Requested { get => requested; set => requested = value; }
            public DateTime Scheduled { get => scheduled; set => scheduled = value; }
            public DateTime PickedUp { get => pickedUp; set => pickedUp = value; }
            public DateTime Delivered { get => delivered; set => delivered = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public override string ToString()
            {
                return $"Id: {Id}, Sender: {Sender}, Target: {Target}, DroneId: {Drone}\nRequested: {Requested}, " +
                    $"Scheduled: {Scheduled}\nPickedUp:  {PickedUp}, Delivered: {Delivered}\nWeight: {Weight}, Priorities:{Priorities}\n";
            }
        }
    }
}

