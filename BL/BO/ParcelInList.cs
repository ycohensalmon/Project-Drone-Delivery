using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInList
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string TargetName { get; set; }
        public ParcelStatuses Status { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priorities { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} \nSenderName: {SenderName} \nTargetName: {TargetName}S\nStatus: {Status} " +
                $"\nWeight: {Weight} \nPriorities:{Priorities}\n";
        }
    }
}
