using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            private int id;
            private int senderId;
            private int targetId;
            private int droneId;
            private DateTime requested;
            private DateTime scheduled;
            private DateTime pickedUp;
            private DateTime delivered;
            private WeightCategory weight;
            private Priority priorities;

            public int Id { get => id; set => id = value; }
            public int SenderId { get => senderId; set => senderId = value; }
            public int TargetId { get => targetId; set => targetId = value; }
            public int DroneId { get => droneId; set => droneId = value; }
            public DateTime Requested { get => requested; set => requested = value; }
            public DateTime Scheduled { get => scheduled; set => scheduled = value; }
            public DateTime PickedUp { get => pickedUp; set => pickedUp = value; }
            public DateTime Delivered { get => delivered; set => delivered = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public override string ToString()
            {
                return $"Id: {Id}, SenderId: {SenderId}, TargetId: {TargetId}, DroneId: {DroneId}\nRequested: {Requested}, " +
                    $"Scheduled: {Scheduled}\nPickedUp:  {PickedUp}, Delivered: {Delivered}\nWeight: {Weight}, Priorities:{Priorities}\n";
            }
        }
    }
}
