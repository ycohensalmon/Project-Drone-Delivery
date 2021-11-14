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
            private Int32 id;
            private string name;
            private Int32 phone;
            private int parcelsScheduledDelivered;
            private int parcelsScheduledNotDelivered;
            private int parcelsRecieved;
            private int parcelsPickedUp;

            public Int32 Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public Int32 Phone { get => phone; set => phone = value; }
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
