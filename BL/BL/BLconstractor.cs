using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;

namespace BL
{
    internal partial class BL : IBL
    {
        public static IBL Instance { get; } = new BL();
        static BL() { }

        internal IDal dalObj;
        Random rand = new Random();
        internal List<DroneInList> drones;
        private double Available, LightParcel, MediumParcel, HeavyParcel, LoadingRate;
        private int HelpSerialNum = 1011;

        public BL()
        {
            drones = new();
            dalObj = DalFactory.GetDal();
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