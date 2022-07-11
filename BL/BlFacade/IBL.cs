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
        /// <summary>
        /// get the drone by his id
        /// </summary>
        /// <param name="droneId">id of the drone </param>
        /// <returns>the drone</returns>
        public Drone GetDroneById(int droneId);

        /// <summary>
        /// get the list of drones
        /// </summary>
        /// <param name="predicate">some condition</param>
        /// <returns>list of drones</returns>
        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null);

        /// <summary>
        /// get the location of the drone in the map
        /// </summary>
        /// <returns>the location</returns>
        public IEnumerable<Location> GetLocationsDrones();
        #endregion

        #region update drone
        /// <summary>
        /// the function receives drone and connect it to the most importent parcel
        /// </summary>
        /// <param name="droneId">the drone to connect</param>
        public void ConnectDroneToParcel(int droneId);

        /// <summary>
        /// the drone take the parcel to delivery
        /// </summary>
        /// <param name="droneId">id of the drone</param>
        public void CollectParcelsByDrone(int droneId);

        /// <summary>
        /// delivery parcel to the client
        /// </summary>
        /// <param name="droneId">id of the drone</param>
        public void DeliveredParcel(int droneId);
        
        /// <summary>
        /// the function send the drone to base charge
        /// </summary>
        /// <param name="droneId">the drone we want send</param>
        /// <returns>the function return the id of the base station that the drone was sent to there</returns>
        public int SendDroneToCharge(int droneId);

        /// <summary>
        /// release the drone from the drone charge
        /// </summary>
        /// <param name="droneId">id of the drone</param>
        public void ReleaseDroneFromCharging(int droneId);

        /// <summary>
        /// change the data of the drone
        /// </summary>
        /// <param name="droneId">id of the drone</param>
        /// <param name="model">new model of the drone</param>
        public void UpdateDrone(int droneId, string model);

        /// <summary>
        /// add a new drone to the list
        /// </summary>
        /// <param name="temp">the new drone</param>
        /// <param name="numStation">the drone that the drone will be added</param>
        public void NewDroneInList(DroneInList temp, int numStation);

        /// <summary>
        /// delete drone from the list
        /// </summary>
        /// <param name="droneId">id of the drone</param>
        public void DeleteDrone(int droneId);
        #endregion

        #endregion

        #region station
        /// <summary>
        /// add a new station to the list
        /// </summary>
        /// <param name="x">the station that the drone will be added</param>
        public void NewStation(Station x);

        /// <summary>
        /// get the id of the station by his name
        /// </summary>
        /// <param name="name">name of station</param>
        /// <returns>id of station</returns>
        public int GetStationIdByName(string name);

        /// <summary>
        /// update the data of the station
        /// </summary>
        /// <param name="num">station</param>
        /// <param name="newName">the new name</param>
        /// <param name="newChargeSolts">the new charge slot avileable</param>
        public void UpdateBase(int num, string newName, string newChargeSolts);
        
        /// <summary>
        /// get the station by his id
        /// </summary>
        /// <param name="stationId">id of the station</param>
        /// <returns>the station</returns>
        public Station GetStationById(int stationId);

        /// <summary>
        /// get list of stations
        /// </summary>
        /// <param name="predicate">some condition</param>
        /// <returns>list of station</returns>
        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null);

        /// <summary>
        /// get list of stattion with avelieble charge slot
        /// </summary>
        /// <returns>list of station with avelieble charge slot</returns>
        public IEnumerable<StationList> GetStationWithChargeSolts();

        /// <summary>
        /// get list names of stations
        /// </summary>
        /// <returns>list of names</returns>
        public IEnumerable<string> GetNamesOfAvailableChargeSolts();

        /// <summary>
        /// get the location of the station
        /// </summary>
        /// <returns>the location</returns>
        public IEnumerable<Location> GetLocationsStation();

        /// <summary>
        /// delete the station
        /// </summary>
        /// <param name="stationId">id of the station</param>
        public void DeleteStation(int stationId);


        #endregion

        #region Customer
        /// <summary>
        /// update the data of the customer
        /// </summary>
        /// <param name="customerID">id of the customer</param>
        /// <param name="newName">the new name of the customer</param>
        /// <param name="newPhone">the new phone number</param>
        public void UpdateCustomer(int customerID, string newName, string newPhone);

        /// <summary>
        /// add a new customer to the list
        /// </summary>
        /// <param name="x">the costumer</param>
        public void NewCostumer(Customer x);

        /// <summary>
        /// get the customer by his id
        /// </summary>
        /// <param name="customerId">id of the customer</param>
        /// <returns>the customer</returns>
        public Customer GetCustomerById(int customerId);

        /// <summary>
        /// get list of customers
        /// </summary>
        /// <param name="predicate">some condition</param>
        /// <returns>list of customers</returns>
        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null);

        /// <summary>
        /// get list names of the customers
        /// </summary>
        /// <returns>list of names</returns>
        public IEnumerable<string> GetNamesOfCustomer();

        /// <summary>
        /// get the customer id by his name
        /// </summary>
        /// <param name="name">name of the customer</param>
        /// <returns>id of the customer</returns>
        public int GetCustomerIdByName(string name);

        /// <summary>
        /// delete the customer from the list
        /// </summary>
        /// <param name="customerId">the id of the customer</param>
        public void DeleteCustomer(int customerId);
        #endregion

        #region parcel

        /// <summary>
        /// add a new parcel to the list
        /// </summary>
        /// <param name="x">the parcel</param>
        /// <param name="senderID">sender id of the parcel</param>
        /// <param name="receiveID">receiver id of the parcel</param>
        public void NewParcel(Parcel x, int senderID, int receiveID);

        /// <summary>
        /// get parcel by his id
        /// </summary>
        /// <param name="parcelid">id of the parcel</param>
        /// <returns>the parcel</returns>
        public Parcel GetParcelById(int parcelid);

        /// <summary>
        /// get a list of parcels that was send <b>from</b> this customer
        /// </summary>
        /// <param name="customerId">the customer id</param>
        /// <returns>list of parcels</returns>
        public IEnumerable<ParcelAtCustomer> GetParcelFromCustomer(int customerId);

        /// <summary>
        /// get a list of parcels that was send <b>to</b> this customer
        /// </summary>
        /// <param name="customerId">the customer id</param>
        /// <returns>list of parcels</returns>
        public IEnumerable<ParcelAtCustomer> GetParcelToCustomer(int customerId);

        /// <summary>
        /// gat a list of parcels
        /// </summary>
        /// <param name="predicate">some condition</param>
        /// <returns>list of parcel</returns>
        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null);

        /// <summary>
        /// get list of parcel that are not in delivery
        /// </summary>
        /// <returns>list of parcel</returns>
        public IEnumerable<ParcelInList> GetParcelsWithoutDrone();

        /// <summary>
        /// delete the parcel from the list
        /// </summary>
        /// <param name="parcelId">id of the parcel</param>
        public void DeleteParcel(int parcelId);
        public DO.Parcel GetParcelWasConnectToParcel(int droneId, out DroneInList drone);
        #endregion

        #region User
        //public void NewParcelOfUser(Parcel x, int receiveID);
        /// <summary>
        /// add user to the list
        /// </summary>
        /// <param name="user">the user to add</param>
        public void NewUser(User user);

        /// <summary>
        /// get the user by his id
        /// </summary>
        /// <param name="userId">id of the user</param>
        /// <returns>the user</returns>
        public User GetUserById(int userId);

        /// <summary>
        /// cheak if the user exist
        /// </summary>
        /// <param name="userId">id of theuser</param>
        /// <param name="password">password of the user</param>
        /// <returns>true if the user exist</returns>
        public bool checkUser(int userId, int password);

        public void confirmPackage(int userId, int parcelId);

        /// <summary>
        /// telete the user from the list
        /// </summary>
        /// <param name="userId">id of the user</param>
        public void DeleteUser(int userId);
        #endregion

        #region Tools

        /// <summary>
        /// get the loading rate
        /// </summary>
        /// <returns>loading rate</returns>
        public double getLoadingRate();

        /// <summary>
        /// get the battery tha the drone loss when he is available (per km)
        /// </summary>
        /// <returns>battery loss</returns>
        public double GetBatteryIossAvailable();

        /// <summary>
        /// get the battery tha the drone loss with a light parcel (per km)
        /// </summary>
        /// <returns></returns>
        public double GetBatteryIossLightParcel();

        /// <summary>
        /// get the battery tha the drone loss with a medium parcel (per km)
        /// </summary>
        /// <returns></returns>
        public double GetBatteryIossMediumParcel();

        /// <summary>
        /// get the battery tha the drone loss with a heavy parcel (per km)
        /// </summary>
        /// <returns></returns>
        public double GetBatteryIossHeavyParcel();

        /// <summary>
        /// clean the file drone charge in the xml file every time that we begginig run tha project
        /// </summary>
        public void ClearDroneCharge();

        public void ActivSimulator(int id, Action updateDelegate, Func<bool> stopDelegate);
        #endregion
    }
}
