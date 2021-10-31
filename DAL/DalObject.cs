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
        // news functions
        public static void NewStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.stations.Add(new Station
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Lattitude = lattitude,
                ChargeSolts = chargeSlots
            });
        }
        public static void NewDrone(int id, int model, int maxWeight)
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
        public static void NewCostumer(int id, string name, int phone, double longitude, double lattitude)
        {
            DataSource.customers.Add(new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latittude = longitude
            });
        }
        public static void NewParcel(int id, int priorities,int weight)
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
        public static void ConnectDroneToParcel(int droneId, int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            DataSource.parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }
        public static void CollectParcelByDrone(int droneId, int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            Drone drone = GetDroneById(droneId);
            DataSource.parcels.Remove(parcel);
            DataSource.drones.Remove(drone);

            drone.Status = DroneStatuses.Delivery;
            parcel.PickedUp = DateTime.Now;

        }


        // Get List
        public static List<Drone> GetDrones() => DataSource.drones;
        public static List<Station> GetStations() => DataSource.stations;
        public static List<Customer> GetCustomers() => DataSource.customers;
        public static List<Parcel> GetParcels() => DataSource.parcels;


        // get lists by id
        public static Station GetStationById(int id) => DataSource.stations.Find(x => x.Id == id);
        public static Drone GetDroneById(int id) => DataSource.drones.Find(x => x.Id == id);
        public static Customer GetCustomerById(int id) => DataSource.customers.Find(x => x.Id == id);
        public static Parcel GetParcelById(int id) => DataSource.parcels.Find(x => x.Id == id);


        // Get Index List
        public static Drone GetIndexDrone(int index) => DataSource.drones[index];
        public static Station GetIndexStation(int index) => DataSource.stations[index];
        public static Customer GetIndexCustomer(int index) => DataSource.customers[index];
        public static Parcel GetIndexParcel(int index) => DataSource.parcels[index];
    }
}
