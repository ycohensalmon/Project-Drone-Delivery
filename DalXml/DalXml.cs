using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalXml : IDal
    {
        internal static IDal Instance { get; } = new DalXml();
        DalXml() { }

        public void NewStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void NewDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void NewCostumer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int NewParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            throw new NotImplementedException();
        }

        public void CollectParcelByDrone(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void DeliveredParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToBaseCharge(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public double ReleaseDroneFromCharging(int droneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(int droneId, string model)
        {
            throw new NotImplementedException();
        }

        public void UpdateBase(int stationId, string newName, string newChargeSolts, int result)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDrones(Func<Drone, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Station GetStationById(int id)
        {
            throw new NotImplementedException();
        }

        public Drone GetDroneById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcelById(int id)
        {
            throw new NotImplementedException();
        }

        public double[] PowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
