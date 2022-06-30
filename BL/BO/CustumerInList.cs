using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BO
    {
        public class CustumerInList
        {
            public Int32 Id { get; set; }
            public string Name { get; set; }
            public Int32 Phone { get; set; }
            public int ParcelsShippedAndDelivered { get; set; }
            public int ParcelsShippedAndNotDelivered { get; set; }
            public int ParcelsHeRecieved { get; set; }
            public int ParcelsOnTheWay { get; set; }
            public bool IsDeleted { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, ParcelsScheduledDelivered:{ParcelsShippedAndDelivered}," +
                    $" ParcelsScheduledNotDelivered:{ParcelsShippedAndNotDelivered}, ParcelsRecieved:{ParcelsHeRecieved}, ParcelsPickedUp:{ParcelsOnTheWay}";
            }

        }
    }
