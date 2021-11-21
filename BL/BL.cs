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
            List<Drone> drones = new();
            public BL()
            {
                IEnumerable<IDAL.DO.Drone> drone = dalObj.GetDrones();
                IEnumerable<IDAL.DO.Station> station = dalObj.GetStations();
                IEnumerable<IDAL.DO.Customer> customer = dalObj.GetCustomers();
                IEnumerable<IDAL.DO.Parcel> parcel = dalObj.GetParcels();
                IEnumerable<IDAL.DO.DroneCharge> droneCharges = dalObj.GetDroneCharges();

                foreach (var tempDrone in drone)
                {
                    drones.Add(new Drone
                    {
                        Id = tempDrone.Id,
                        Model = tempDrone.Model,
                        MaxWeight = (WeightCategory)tempDrone.MaxWeight,
                        
                    });
                }

                // אם החבלילה לא סופקה אך שוייכה
                foreach (var tempParcel in parcel)
                {
                    if (tempParcel.DroneId == drones.Id)
                    {
                        drones.Battery = rand.Next(30, 70);
                        drones.Status = DroneStatuses.Delivery;
                        // החבילה שויכה ולא נאספה
                        if (tempParcel.Scheduled != DateTime.MinValue && tempParcel.PickedUp == DateTime.MinValue)
                        {
                            // מיקום - תחנה הקרובה לשולח
                        }
                        // חבילה נאספה אך לא סופקה
                        else if (tempParcel.PickedUp != DateTime.MinValue)
                        {
                            // מיקום - מיקום השולח
                        }
                    }
                    else // כאשר הרחפן לא במשלוח
                    {
                        drones.Status = (DroneStatuses)rand.Next(2); // מגריל בין תחזוקה לפנוי

                    }

                }
            }
        }
    }
}
