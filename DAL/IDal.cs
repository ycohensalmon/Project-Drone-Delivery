using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalFacade
{
    public interface IDal
    {
        /// <summary>
        /// add new station to the list
        /// </summary>
        /// <param name="station">the paraneter to adding</param>
        public void NewStation(Station station);

        /// <summary>
        /// adds a drone to the list of Drones
        /// </summary>
        /// <param name="drone">the paraneter to adding</param>
        public void NewDrone(Drone drone);

        /// <summary>
        /// adds a customer to list of Customers
        /// </summary>
        /// <param name="customer">the paraneter to adding</param>
        public void NewCostumer(Customer customer);

        /// <summary>
        /// adds a parcel to the list of Parcels
        /// </summary>
        /// <param name="parcel">the paraneter to adding</param>
        public void NewParcel(Parcel parcel);

        /// <summary>
        /// assign a drone to a parcel and update the scheduled time
        /// </summary>
        /// <param name="droneId">the id of the parcel</param>
        /// <param name="parcelId">the id of the drone</param>
        public void ConnectDroneToParcel(int droneId, int parcelId);

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void CollectParcelByDrone(int parcelId);

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void DeliveredParcel(int parcelId);

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="droneId">the drone to send to charge</param>
        /// <param name="stationId">the station to send it to charge</param>
        public void SendDroneToBaseCharge(int droneId, int stationId);

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="droneId">the id of the drone to release</param>
        public double ReleaseDroneFromCharging(int droneId);

        /// <summary>
        /// Update the name of the drone
        /// </summary>
        /// <param name="droneId">Id of the drone that we want to change his model</param>
        /// <param name="model">The new name of the model</param>
        public void UpdateDrone(int droneId, string model);

        /// <summary>
        /// Update the name of the base station or the number of charge slot available or together
        /// </summary>
        /// <param name="stationId">Id of the base station that we want to change his name</param>
        /// <param name="newName">the new name</param>
        /// <param name="newChargeSolts">the new chagre slot available in string</param>
        /// <param name="result">the new chagre slot available in int</param>
        public void UpdateBase(int stationId, string newName, string newChargeSolts, int result);

        /// <summary>
        /// update the name or phone of the customer
        /// </summary>
        /// <param name="customerID">Id of the customer that we want to modified</param>
        /// <param name="newName">the new name</param>
        /// <param name="newPhone">the new phone</param>
        public void UpdateCustomer(int customerID, string newName, string newPhone);


        /// <summary>
        /// Get the Drones
        /// </summary>
        /// <param name="predicate">can be</param>
        /// <returns>the lists of the Drones </returns>
        public IEnumerable<Drone> GetDrones(Func<Drone, bool> predicate = null);

        /// <summary>
        /// Get the Stations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>the lists of the Stations</returns>
        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null);

        /// <summary>
        /// Get the Customers
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>the lists of the Customers </returns>
        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null);

        /// <summary>
        /// Get the Parcels
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>the lists of the Parcels </returns>
        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null);

        /// <summary>
        /// Get the Drones charge
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>the lists of the Drones </returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null);

        /// <summary>
        /// get station with this id and if no, error is thrown
        /// </summary>
        /// <param name="id">the id of the Stations</param>
        /// <returns>station with this id</returns>
        public Station GetStationById(int id);

        /// <summary>
        /// get drone with this id and if no, error is thrown
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <returns>drone cwith this id</returns>
        public Drone GetDroneById(int id);

        /// <summary>
        /// get customer with this id and if no, error is thrown
        /// </summary>
        /// <param name="id">the id of the Customer</param>
        /// <returns>customer with this id</returns>
        public Customer GetCustomerById(int id);

        /// <summary>
        /// get parcel with this id and if no, error is thrown
        /// </summary>
        /// <param name="id">the id of the Parcels</param>
        /// <returns>parcel with this id</returns>
        public Parcel GetParcelById(int id);

        /// <summary>
        /// Transfers in the array from the data layer to the logic layer
        /// the battery consumption data for all types of weights
        /// </summary>
        /// <returns>Array with percentage of battery consumption all types of weights</returns>
        public double[] PowerConsumptionByDrone();
    }
}
