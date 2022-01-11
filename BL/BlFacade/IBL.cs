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
        //public void NewParcelOfUser(Parcel x, int receiveID);
        public void NewDroneInList(DroneInList temp, int numStation);
        /// <summary>
        /// the function receives drone and connect it to the most importent parcel
        /// </summary>
        /// <param name="droneId">the drone to connect</param>
        public void ConnectDroneToParcel(int droneId);
        public void CollectParcelsByDrone(int droneId);
        public void DeliveredParcel(int droneId);
        /// <summary>
        /// the function send the drone to base charge
        /// </summary>
        /// <param name="droneId">the drone we want send</param>
        /// <returns>the function return the id of the base station that the drone was sent to there</returns>
        public int SendDroneToCharge(int droneId);
        public void ReleaseDroneFromCharging(int droneId);
        public void UpdateDrone(int droneId, string model);
        public void UpdateBase(int num, string newName, string newChargeSolts);
        public void UpdateCustomer(int customerID, string newName, string newPhone);
        public Station GetStationById(int stationId);
        public Drone GetDroneById(int droneId);
        public Customer GetCustomerById(int customerId);
        public Parcel GetParcelById(int parcelid);
        public User GetUserById(int userId);
        public IEnumerable<string> GetNamesOfCustomer();
        public IEnumerable<string> GetNamesOfAvailableChargeSolts();
        public int GetStationIdByName(string name);
        public int GetCustomerIdByName(string name);
        public IEnumerable<ParcelAtCustomer> GetParcelFromCustomer(int customerId);
        public IEnumerable<ParcelAtCustomer> GetParcelToCustomer(int customerId);
        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null);
        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null);
        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null);
        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null);
        public IEnumerable<ParcelInList> GetParcelsWithoutDrone(); 
        public IEnumerable<StationList> GetStationWithChargeSolts();
        public DO.Parcel GetParcelWasConnectToParcel(int droneId, out DroneInList drone);
        public void ClearDroneCharge();
        public bool checkUser(int userId, int password);
        public void confirmPackage(int userId, int parcelId);
        public double getLoadingRate();
        public double GetBatteryIossAvailable();
        public double GetBatteryIossLightParcel();
        public double GetBatteryIossMediumParcel();
        public double GetBatteryIossHeavyParcel();
        public void ActivSimulator(int id, Action updateDelegate, Func<bool> stopDelegate);

    }
}
