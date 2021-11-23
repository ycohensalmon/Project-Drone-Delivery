using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDAL.IDal
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
        public void NewStation(Station x)
        {
            foreach (var item in collection)
            {

            }
            if (x.Id == )
            {

            }
            DataSource.Stations.Add(x);
        }

        /// <summary>
        /// adds a drone to the list of Drones
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewDrone(Drone x) => DataSource.Drones.Add(x);

        /// <summary>
        /// adds a customer to list of Customers
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewCostumer(Customer x) => DataSource.Customers.Add(x);

        /// <summary>
        /// adds a parcel to the list of Parcels
        /// </summary>
        /// <param name="x">the paraneter to adding</param>
        public void NewParcel(Parcel x) => DataSource.Parcels.Add(x);


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
            DataSource.Parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;

            DataSource.Parcels.Add(parcel);
        }
        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void CollectParcelByDrone(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            Drone drone = DataSource.Drones.Find(x => x.Id == parcel.DroneId);
            DataSource.Parcels.Remove(parcel);
            DataSource.Drones.Remove(drone);

            parcel.PickedUp = DateTime.Now;
           // drone.Status = DroneStatuses.Delivery;

            DataSource.Drones.Add(drone);
            DataSource.Parcels.Add(parcel);
        }
        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="parcelId">the id of the parcel</param>
        public void DeliveredParcel(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            Drone drone = DataSource.Drones.Find(x => x.Id == parcel.DroneId);
            DataSource.Parcels.Remove(parcel);
            DataSource.Drones.Remove(drone);

            //drone.Status = DroneStatuses.Available;
            parcel.Delivered = DateTime.Now;
            parcel.DroneId = 0;

            DataSource.Drones.Add(drone);
            DataSource.Parcels.Add(parcel);
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
            DataSource.Stations.Remove(station);

            DataSource.DroneCharges.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id
            });
            station.ChargeSolts--;

            DataSource.Stations.Add(station);
        }
        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="droneId">the id of the drone to release</param>
        public void ReleaseDroneFromCharging(int droneId)
        {
            Drone drone = GetDroneById(droneId);
            DataSource.Drones.Remove(drone);

            DroneCharge droneCharge = DataSource.DroneCharges.Find(x => x.DroneId == droneId);
            int stationId = droneCharge.StationId;  //?
            Station station = GetStationById(droneId);
            DataSource.Stations.Remove(station);

            station.ChargeSolts++;
            //drone.Status = DroneStatuses.Available;
            //drone.Battery = 100;

            DataSource.Stations.Add(station);
            DataSource.Drones.Add(drone);
            DataSource.DroneCharges.Remove(droneCharge);

        }


        //-----------------------------------------------------------------------------------------------------------//
                                                      // Get List //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Get the Drones
        /// </summary>
        /// <returns>the lists of the Drones </returns>
        public IEnumerable<Drone> GetDrones() => DataSource.Drones;
        
        /// <summary>
        /// Get the Drones charge
        /// </summary>
        /// <returns>the lists of the Drones </returns>
        public IEnumerable<DroneCharge> GetDroneCharges() => DataSource.DroneCharges;

        /// <summary>
        /// Get the Stations
        /// </summary>
        /// <returns>the lists of the Stations</returns>
        public IEnumerable<Station> GetStations() => DataSource.Stations;

        /// <summary>
        /// Get the Customers
        /// </summary>
        /// <returns>the lists of the Customers </returns>
        public IEnumerable<Customer> GetCustomers() => DataSource.Customers;

        /// <summary>
        /// Get the Parcels
        /// </summary>
        /// <returns>the lists of the Parcels </returns>
        public IEnumerable<Parcel> GetParcels() => DataSource.Parcels;


        //-----------------------------------------------------------------------------------------------------------//
                                                  // get objects by id //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// returns the object Station that matches the id
        /// </summary>
        /// <param name="id">the id of the Stations</param>
        /// <returns></returns>
        public Station GetStationById(int id) => DataSource.Stations.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Customer that matches the id
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <returns>x</returns>
        public Drone GetDroneById(int id) => DataSource.Drones.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Customer that matches the id
        /// </summary>
        /// <param name="id">the id of the Customer</param>
        /// <returns></returns>
        public Customer GetCustomerById(int id) => DataSource.Customers.Find(x => x.Id == id);

        /// <summary>
        /// returns the object Parcels that matches the id
        /// </summary>
        /// <param name="id">the id of the Parcels</param>
        /// <returns></returns>
        public Parcel GetParcelById(int id) => DataSource.Parcels.Find(x => x.Id == id);

        //-----------------------------------------------------------------------------------------------------------//
                                                 // other //
        //-----------------------------------------------------------------------------------------------------------//

        public IEnumerable<double> PowerConsumptionByDrone()
        {
            yield return DataSource.Config.Available;
            yield return DataSource.Config.LightParcel;
            yield return DataSource.Config.MediumParcel;
            yield return DataSource.Config.HeavyParcel;
            yield return DataSource.Config.LoadingRate;
        }
    }
}
