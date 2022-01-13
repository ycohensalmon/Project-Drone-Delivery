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
        #region drone

        #region getting
        public Drone GetDroneById(int droneId);
        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null);
        public IEnumerable<Location> GetLocationsDrones();
        #endregion

        #region update drone
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
        public void NewDroneInList(DroneInList temp, int numStation);
        #endregion

        #endregion

        #region station
        public void NewStation(Station x);
        public int GetStationIdByName(string name);
        public void UpdateBase(int num, string newName, string newChargeSolts);
        public Station GetStationById(int stationId);
        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null);
        public IEnumerable<StationList> GetStationWithChargeSolts();
        public IEnumerable<string> GetNamesOfAvailableChargeSolts();
        #endregion
        
        #region Costumer
        public void UpdateCustomer(int customerID, string newName, string newPhone);
        public void NewCostumer(Customer x);
        public Customer GetCustomerById(int customerId);
        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null);
        public IEnumerable<string> GetNamesOfCustomer();
        public int GetCustomerIdByName(string name);
        #endregion
        
        #region parcel
        public void NewParcel(Parcel x, int senderID, int receiveID);
        public Parcel GetParcelById(int parcelid);
        public IEnumerable<ParcelAtCustomer> GetParcelFromCustomer(int customerId);
        public IEnumerable<ParcelAtCustomer> GetParcelToCustomer(int customerId);
        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null);
        public IEnumerable<ParcelInList> GetParcelsWithoutDrone(); 
        public DO.Parcel GetParcelWasConnectToParcel(int droneId, out DroneInList drone);
        #endregion

        #region User
        //public void NewParcelOfUser(Parcel x, int receiveID);
        public User GetUserById(int userId);
        public bool checkUser(int userId, int password);
        public void confirmPackage(int userId, int parcelId);
        #endregion

        #region Tools
        public double getLoadingRate();
        public double GetBatteryIossAvailable();
        public double GetBatteryIossLightParcel();
        public double GetBatteryIossMediumParcel();
        public double GetBatteryIossHeavyParcel();
        public void ClearDroneCharge();
        public void ActivSimulator(int id, Action updateDelegate, Func<bool> stopDelegate);
        #endregion
    }
}
