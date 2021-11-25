using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustumerInList
        {
            private int parcelsScheduledDelivered;
            private int parcelsScheduledNotDelivered;
            private int parcelsRecieved;
            private int parcelsPickedUp;

            public Int32 Id { get; set; }
            public string Name { get; set; }
            public Int32 Phone { get; set; }
            public int ParcelsScheduledDelivered { get; set; }
            public int ParcelsScheduledNotDelivered { get; set; }
            public int ParcelsRecieved { get; set; }
            public int ParcelsPickedUp { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, ParcelsScheduledDelivered:{ParcelsScheduledDelivered}," +
                    $" ParcelsScheduledNotDelivered:{ParcelsScheduledNotDelivered}, ParcelsRecieved:{ParcelsRecieved}, ParcelsPickedUp:{ParcelsPickedUp}";
            }

        }
    }
}
