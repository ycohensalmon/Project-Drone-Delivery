using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInList
        {
            private int id;
            private string senderName;
            private string targetName;
            private DateTime requested;
            private DateTime scheduled;
            private DateTime pickedUp;
            private DateTime delivered;
            private WeightCategory weight;
            private Priority priorities;

            public int Id { get => id; set => id = value; }
            public string SenderName { get => senderName; set => senderName = value; }
            public string TargetName { get => targetName; set => targetName = value; }
            public DateTime Requested { get => requested; set => requested = value; }
            public DateTime Scheduled { get => scheduled; set => scheduled = value; }
            public DateTime PickedUp { get => pickedUp; set => pickedUp = value; }
            public DateTime Delivered { get => delivered; set => delivered = value; }
            public WeightCategory Weight { get => weight; set => weight = value; }
            public Priority Priorities { get => priorities; set => priorities = value; }
            public override string ToString()
            {
                return $"Id: {Id}, SenderName: {SenderName}, TargetName: {TargetName},S\nRequested: {Requested}, " +
                    $"Scheduled: {Scheduled}\nPickedUp:  {PickedUp}, Delivered: {Delivered}\nWeight: {Weight}, Priorities:{Priorities}\n";
            }
        }
    }
}
