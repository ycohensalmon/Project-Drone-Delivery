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
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public DroneInParcel Drone { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public WeightCategory Weight { get; set; }
            public Priority Priorities { get; set; }
            public override string ToString()
            {
                if (Drone.Id != 0)
                {
                    return $"Id: {Id}, \nSender: {Sender} \nTarget: {Target} \nDroneId: {Drone}\nRequested: {Requested} " +
                        $"\nScheduled: {Scheduled}\nPickedUp:  {PickedUp} \nDelivered: {Delivered}\nWeight: {Weight} Priorities:{Priorities}\n";
                }
                return $"Id: {Id} \nSender: {Sender} \nTarget: {Target} \nRequested: {Requested} " +
                    $"\nScheduled: {Scheduled}\nPickedUp:  {PickedUp} \nDelivered: {Delivered}\nWeight: {Weight} \nPriorities:{Priorities}\n";
            }
        }
    }
}

