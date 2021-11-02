using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        /// <summary>
        /// constructor for DalObject class
        /// </summary>
        public DalObject() => DataSource.Initialize();



        //-----------------------------------------------------------------------------------------------------------//
                                                      // news functions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// add new station to the list
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewStation(Station x) => DataSource.stations.Add(x);

        /// <summary>
        /// adds a drone to the list of drones
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewDrone(Drone x) => DataSource.drones.Add(x);

        /// <summary>
        /// adds a customer to list of customers
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewCostumer(Customer x) => DataSource.customers.Add(x);

        /// <summary>
        /// adds a parcel to the list of parcels
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewParcel(Parcel x) => DataSource.parcels.Add(x);


        //-----------------------------------------------------------------------------------------------------------//
                                                    // uptades fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// assign a drone to a parcel and update the scheduled time
        /// </summary>
        /// <param name="droneId">the id of the parcel</param>
        /// <param name="parcelId">the id of the drone</param>
        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;

            DataSource.parcels.Add(parcel);
        }
        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void CollectParcelByDrone(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            Drone drone = DataSource.drones.Find(x => x.Id == parcel.DroneId);
            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            parcel.PickedUp = DateTime.Now;
            drone.Status = DroneStatuses.Delivery;

            DataSource.drones.Add(drone);
            DataSource.parcels.Add(parcel);
        }
        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void DeliveredParcel(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            Drone drone = DataSource.drones.Find(x => x.Id == parcel.DroneId);
            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            drone.Status = DroneStatuses.Available;
            parcel.Delivered = DateTime.Now;
            parcel.DroneId = 0;

            DataSource.drones.Add(drone);
            DataSource.parcels.Add(parcel);
        }
        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="droneId">the drone to send to charge</param>
        /// <param name="stationId">the station to send it to charge</param>
        public void SendDroneToBaseCharge(int droneId, int stationId)
        {
            Drone drone = GetDroneById(droneId);
            Station station = GetStationById(stationId);
            DataSource.stations.Remove(station);
            DataSource.drones.Remove(drone);

            DataSource.droneCharges.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id
            });
            station.ChargeSolts--;
            drone.Status = DroneStatuses.Maintenance;

            DataSource.stations.Add(station);
            DataSource.drones.Add(drone);
        }
        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="droneId">the id of the drone to release</param>
        public void ReleaseDroneFromCharging(int droneId)
        {
            Drone drone = GetDroneById(droneId);
            DataSource.drones.Remove(drone);

            DroneCharge droneCharge = DataSource.droneCharges.Find(x => x.DroneId == droneId);
            int stationId = droneCharge.StationId;  //?
            Station station = GetStationById(droneId);
            DataSource.stations.Remove(station);

            station.ChargeSolts++;
            drone.Status = DroneStatuses.Available;
            drone.Battery = 100;

            DataSource.stations.Add(station);
            DataSource.drones.Add(drone);
            DataSource.droneCharges.Remove(droneCharge);

        }


        //-----------------------------------------------------------------------------------------------------------//
                                                      // Get List //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Get the Drones
        /// </summary>
        /// <returns>the lists of the Drones </returns>
        public List<Drone> GetDrones() => DataSource.drones;

        /// <summary>
        /// Get the Stations
        /// </summary>
        /// <returns>the lists of the stations</returns>
        public List<Station> GetStations() => DataSource.stations;

        /// <summary>
        /// Get the customers
        /// </summary>
        /// <returns>the lists of the customers </returns>
        public List<Customer> GetCustomers() => DataSource.customers;

        /// <summary>
        /// Get the parcels
        /// </summary>
        /// <returns>the lists of the parcels </returns>
        public List<Parcel> GetParcels() => DataSource.parcels;


        //-----------------------------------------------------------------------------------------------------------//
                                                  // get objects by id //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// returns the object Station that matches the id
        /// </summary>
        /// <param name="id">the id of the Stations</param>
        /// <returns></returns>
        public Station GetStationById(int id) => DataSource.stations.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Customer that matches the id
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <returns>x</returns>
        public Drone GetDroneById(int id) => DataSource.drones.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Customer that matches the id
        /// </summary>
        /// <param name="id">the id of the Customer</param>
        /// <returns></returns>
        public Customer GetCustomerById(int id) => DataSource.customers.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Parcels that matches the id
        /// </summary>
        /// <param name="id">the id of the Parcels</param>
        /// <returns></returns>
        public Parcel GetParcelById(int id) => DataSource.parcels.Find(x => x.Id == id);
    }
}
