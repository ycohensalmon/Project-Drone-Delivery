using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBL
    {
        public void NewStation(Station x);
        public void NewCostumer(Customer x);
        public void NewParcel(Parcel x, int senderID, int receiveID);
        public void NewDroneInList(DroneInList temp, int numStation);
        public void ConnectDroneToParcel(int droneId);
        public void CollectParcelsByDrone(int droneId);
        public void DeliveredParcel(int droneId);
        public void SendDroneToCharge(int droneId);
        public void UpdateDrone(int droneId, string model);
        public void ReleaseDroneFromCharging(int droneId);
        public void UpdateBase(int num, string newName, string newChargeSolts);
        public void UpdateCustomer(int customerID, string newName, string newPhone);
        public Station GetStationById(int stationId);
        public Drone GetDroneById(int droneId);
        public Customer GetCustomerById(int customerId);
        public Parcel GetParcelById(int parcelid);
        public User GetUserById(int userId);
        public IEnumerable<string> GetNamesOfCustomer(Predicate<string> predicate = null);
        public IEnumerable<string> GetNamesOfAvailableChargeSolts();
        public int GetStationIdByName(string name);
        public int GetCustomerIdByName(string name);
        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null);
        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null);
        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null);
        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null);
        public IEnumerable<ParcelInList> GetParcelsWithoutDrone(); 
        public IEnumerable<StationList> GetStationWithChargeSolts();
        public DO.Parcel GetParcelWasConnectToParcel(int droneId, out DroneInList drone);
    }
}
