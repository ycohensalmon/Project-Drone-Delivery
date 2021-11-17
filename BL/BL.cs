using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;


namespace IBL
{
    namespace BO
    {
        public class BL : IBL
        {
            Random rand = new Random();
            public IDal dalObj = new DalObject.DalObject();
            public ParcelInList ParcelList;// = IDAL.DO.Parcel;
            public BL()
            {
                IEnumerable<IDAL.DO.Drone> drone = dalObj.GetDrones();
                IEnumerable<IDAL.DO.Station> station = dalObj.GetStations();
                IEnumerable<IDAL.DO.Customer> customer = dalObj.GetCustomers();
                IEnumerable<IDAL.DO.Parcel> parcel = dalObj.GetParcels();

                Drone drones = new();
                foreach (var tempDrone in drone)
                {
                    drones.Id = tempDrone.Id;
                    drones.Model = tempDrone.Model;
                    drones.MaxWeight = (WeightCategory)tempDrone.MaxWeight;
                    drones.Battery = rand.Next(30, 70);

                    // אם הרחפן שוייך לחבילה
                    parcel.First(parcel => parcel.DroneId == tempDrone.Id); 

                    //...
                }

                DateTime nulValue = DateTime.MinValue;
                foreach (var x in parcel) if (x.Delivered == nulValue && x.DroneId != 0) // חבילה לא סופקה והרחפן משוייך
                {
                        // סטטוס רחפן כמבצע משלוח
                        if (x.Scheduled != nulValue && x.PickedUp == nulValue) // החבילה שויכה ולא נאספה
                        {
                            // מיקום - תחנה הקרובה לשולח
                        }
                        if (x.PickedUp != nulValue && x.Delivered == nulValue) // חבילה נאספה אך לא סופקה
                        {
                            // מיקום - מיקום השולח
                        }

                }
            }
        }
    }
}
