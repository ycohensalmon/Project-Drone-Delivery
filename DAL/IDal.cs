using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        public void NewStation(Station x);
        public void NewDrone(Drone x);
        public void NewCostumer(Customer x);
        public void NewParcel(Parcel x);
        public void ConnectDroneToParcel(int droneId, int parcelId);
        public void CollectParcelByDrone(int parcelId);
        public void DeliveredParcel(int parcelId);
        public void SendDroneToBaseCharge(int droneId, int stationId);
        public void ReleaseDroneFromCharging(int droneId);
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Station> GetStations();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<DroneCharge> GetDroneCharges();
        public Station GetStationById(int id);
        public Drone GetDroneById(int id);
        public Customer GetCustomerById(int id);
        public Parcel GetParcelById(int id);
        public double[] PowerConsumptionByDrone();
        public void UpdateDrone(int droneId, string model);
    }
}
