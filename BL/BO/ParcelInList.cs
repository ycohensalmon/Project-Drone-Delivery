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
            public Int32 Id { get; set; }
            public string SenderName { get; set; }
            public string TargetName { get; set; }
            public DateTime? Requested { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public WeightCategory Weight { get; set; }
            public Priority Priorities { get; set; }
            public override string ToString()
            {
                return $"Id: {Id}, SenderName: {SenderName}, TargetName: {TargetName},S\nRequested: {Requested}, " +
                    $"Scheduled: {Scheduled}\nPickedUp:  {PickedUp}, Delivered: {Delivered}\nWeight: {Weight}, Priorities:{Priorities}\n";
            }
        }
    }
}
