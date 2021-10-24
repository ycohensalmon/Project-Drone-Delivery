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
            private ParcelStatus requested;
            private ParcelStatus scheduled;
            private ParcelStatus pickedUp;
            private ParcelStatus delivered;
            private WeightCategory weight;
            private Priority priorities;

            public int Id { get => id; set => id = value; }
            public int SenderId { get => senderId; set => senderId = value; }
            public int TargetId { get => targetId; set => targetId = value; }
            public int DroneId { get => droneId; set => droneId = value; }
            public ParcelStatus Requested { get => requested; set => requested = value; }
            public ParcelStatus Scheduled { get => scheduled; set => scheduled = value; }
            public ParcelStatus PickedUp { get => pickedUp; set => pickedUp = value; }
            public ParcelStatus Delivered { get => delivered; set => delivered = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public override string ToString()
            {
                return $"Id:{Id}, SenderId:{SenderId}, TargetId:{TargetId}, DroneId:{DroneId}, Requested:{Requested}," +
                    $"Scheduled:{Scheduled}, PickedUp:{PickedUp}, Delivered:{Delivered}, Weight:{Weight}, Priorities:{Priorities}";
            }
        }
    }
}
