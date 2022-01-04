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
        #region singelton
        // =null that if we dont need to create a "new bl" it will not create it
        private static BL instance = null;
        // for safty. So that if requests come from two places at the same time, it will not create it twice 
        private static readonly object padlock = new object();

        public static BL Instance
        {
            get
            {
                //if "instance" hasn`t yet been created, a new one will be created 
                if (instance == null)
                {
                    //stops a request from two places at the same time
                    lock(padlock)
                    {
                        if (instance == null)
                        {
                            instance = new BL();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        internal IDal dalObj;
        Random rand = new Random();
        internal List<DroneInList> drones;
        private double Available, LightParcel, MediumParcel, HeavyParcel, LoadingRate;
        private int HelpSerialNum = 1011;

        public BL()
        {
            //intializing data with a DalObject
            dalObj = DalFactory.GetDal();

            drones = new();

            //getting electricity consumption of drones 
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

        public void ClearDroneCharge() { dalObj.ClearDroneCharge(); }
    }
}
