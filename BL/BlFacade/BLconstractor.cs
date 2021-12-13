using DalFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalFacade;
using BL.BO;

namespace BL
{
    namespace BlFacade
    {
        internal partial class BL : IBL
        {
            private static readonly BL instance = new BL();
            static BL() { }
            public static BL Instance { get => instance; }

            private readonly IBL myDal = BlFactory.GetBL();


            Random rand = new Random();
            private IDal dalObj;
            internal List<DroneInList> drones;
            private double Available, LightParcel, MediumParcel, HeavyParcel, LoadingRate;

            public BL()
            {
                drones = new();
                dalObj = new DalObject.DalObject();
                Available = dalObj.PowerConsumptionByDrone()[0];
                LightParcel = dalObj.PowerConsumptionByDrone()[1];
                MediumParcel = dalObj.PowerConsumptionByDrone()[2];
                HeavyParcel = dalObj.PowerConsumptionByDrone()[3];
                LoadingRate = dalObj.PowerConsumptionByDrone()[4];

                
                IEnumerable<DO.Drone> drone = dalObj.GetDrones();
                IEnumerable<DO.Station> station = dalObj.GetStations();
                IEnumerable<DO.Customer> customer = dalObj.GetCustomers();
                IEnumerable<DO.Parcel> parcel = dalObj.GetParcels();

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
