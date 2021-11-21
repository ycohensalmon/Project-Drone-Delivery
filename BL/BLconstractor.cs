using IDAL;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
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
                    DroneStatuses statuses = GetStatus(tempDrone.Id, parcel);

                    drones.Add(new Drone
                    {
                        Id = tempDrone.Id,
                        Model = tempDrone.Model,
                        MaxWeight = (WeightCategory)tempDrone.MaxWeight,
                        Status = statuses,
                        Battery = GetBattery(statuses),
                        //ParcelInTravel =
                        Location = GetLocation(statuses, tempDrone.Id, parcel)
                    });
                }
            }

            private Location GetLocation(DroneStatuses statuses, int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                IDAL.DO.Parcel tempParcel = GetTempParcel(droneId, parcel);
                if (tempParcel.DroneId == droneId)
                {
                    // החבילה שויכה ולא נאספה
                    if (tempParcel.Scheduled != DateTime.MinValue && tempParcel.PickedUp == DateTime.MinValue)
                    {
                        // מיקום - תחנה הקרובה לשולח
                    }
                    // חבילה נאספה אך לא סופקה
                    else if (tempParcel.PickedUp != DateTime.MinValue && tempParcel.Delivered == DateTime.MinValue)
                    {
                        // מיקום - מיקום השולח
                    }
                }

                if (statuses == DroneStatuses.Available)
                {
                    List<IDAL.DO.Parcel> parcelDelivered = new();
                    foreach (var item in parcel) if (item.Delivered != DateTime.MinValue) { parcelDelivered.Add(item); }
                }
            }

            private double GetBattery(DroneStatuses status)
            {
                if (status != DroneStatuses.Maintenance) return rand.Next(30, 100);
                else return rand.Next(0, 20);
            }

            private DroneStatuses GetStatus(int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                if (GetTempParcel(droneId, parcel).DroneId == droneId)
                    return DroneStatuses.Delivery;
                else
                    return (DroneStatuses)rand.Next(2);
            }

            private IDAL.DO.Parcel GetTempParcel(int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                return parcel.First(parcel => parcel.DroneId == droneId);
            }
        }
    }
}
