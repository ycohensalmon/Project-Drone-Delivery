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
            public IDal dalObj;
            public List<DroneInList> drones;
            public double Available, LightParcel, MediumParcel, HeavyParcel, LoadingRate;

            public BL()
            {
                drones = new();
                dalObj = new DalObject.DalObject();
                Available = dalObj.PowerConsumptionByDrone()[0];
                LightParcel = dalObj.PowerConsumptionByDrone()[1];
                MediumParcel = dalObj.PowerConsumptionByDrone()[2];
                HeavyParcel = dalObj.PowerConsumptionByDrone()[3];
                LoadingRate = dalObj.PowerConsumptionByDrone()[4];

                
                IEnumerable<IDAL.DO.Drone> drone = dalObj.GetDrones();
                IEnumerable<IDAL.DO.Station> station = dalObj.GetStations();
                IEnumerable<IDAL.DO.Customer> customer = dalObj.GetCustomers();
                IEnumerable<IDAL.DO.Parcel> parcel = dalObj.GetParcels();

                foreach (var tempDrone in drone)
                {
                    DroneStatuses statuses = GetStatus(tempDrone.Id, parcel);

                    drones.Add(new DroneInList
                    {
                        Id = tempDrone.Id,
                        Model = tempDrone.Model,
                        MaxWeight = (WeightCategory)tempDrone.MaxWeight,
                        Status = statuses,
                        Battery = GetBattery(statuses),
                        Location = GetLocation(statuses, tempDrone.Id, parcel, customer, station),
                        NumParcel = GetNumParcel(statuses, tempDrone.Id, parcel)
                    });
                }
            }
        }
    }
}
