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


        // news functions
        public void NewStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.stations.Add(new Station
            {
                Id = id,
                Name = name,
                Longitude = DataSource.Sexagesimal(longitude,'N'),
                Lattitude = DataSource.Sexagesimal(lattitude, 'E'),
                ChargeSolts = chargeSlots
            });
        }
        public void NewDrone(int id, int model, int maxWeight)
        {
            DataSource.drones.Add(new Drone
            {
                Id = id,
                Model = (ModelDrones)model,
                MaxWeight = (WeightCategory)maxWeight,
                Status = DroneStatuses.Available,
                Battery = 100
            });
        }
        public void NewCostumer(int id, string name, int phone, double longitude, double lattitude)
        {
            DataSource.customers.Add(new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = DataSource.Sexagesimal(longitude, 'N'),
                Latittude = DataSource.Sexagesimal(lattitude, 'E') 
            });
        }
        public void NewParcel(int id, int priorities, int weight)
        {
            DataSource.parcels.Add(new Parcel
            {
                Id = id,
                SenderId = 0,
                TargetId = 0,
                DroneId = 0,
                Requested = DateTime.Now,
                Scheduled = DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Delivered = DateTime.MinValue,
                Weight = (WeightCategory)weight,
                Priorities = (Priority)priorities
            });
        }


        // uptades fonctions

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


        // Get List

        /// <summary>
        /// Get the Drones, Stations, Customers and the Parcels
        /// </summary>
        /// <returns>the lists of the Drones, Stations, Customers and the Parcels </returns>
        public List<Drone> GetDrones() => DataSource.drones;
        public List<Station> GetStations() => DataSource.stations;
        public List<Customer> GetCustomers() => DataSource.customers;
        public List<Parcel> GetParcels() => DataSource.parcels;


        // get objects by id

        /// <summary>
        /// get 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Station GetStationById(int id) => DataSource.stations.Find(x => x.Id == id);
        public Drone GetDroneById(int id) => DataSource.drones.Find(x => x.Id == id);
        public Customer GetCustomerById(int id) => DataSource.customers.Find(x => x.Id == id);
        public Parcel GetParcelById(int id) => DataSource.parcels.Find(x => x.Id == id);


        // Get Index List
        public Drone GetIndexDrone(int index) => DataSource.drones[index];
        public Station GetIndexStation(int index) => DataSource.stations[index];
        public Customer GetIndexCustomer(int index) => DataSource.customers[index];
        public Parcel GetIndexParcel(int index) => DataSource.parcels[index];
    }
}
