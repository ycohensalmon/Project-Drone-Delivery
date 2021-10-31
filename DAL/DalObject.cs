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
        //public DalObject() { DataSource.Initialize(); }
        public static List<Drone> GetDrones() => DataSource.drones;
        public static List<Station> GetStations() => DataSource.stations;
        public static List<Customer> GetCustomers() => DataSource.customers;
        public static List<Parcel> GetParcels() => DataSource.parcels;
        public static Drone GetIndexDrone(int index) => DataSource.drones[index];
        public static Station GetIndexStation(int index) => DataSource.stations[index];
        public static Customer GetIndexCustomer(int index) => DataSource.customers[index];
        public static Parcel GetIndexParcel(int index) => DataSource.parcels[index];
        public static Drone GetDroneById(int id)
        {
            int i = 0;
            for (; i < GetDrones().Count; i++)
                if (GetIndexDrone(i).Id == id)
                    break;

            return GetIndexDrone(i);
        }
        public static Parcel GetParcelById(int id)
        {
            int i = 0;
            for (; i < GetParcels().Count; i++)
                if (GetIndexParcel(i).Id == id)
                    break;

            return GetIndexParcel(i);
        }
        public static void ConnectDroneToParcel(int droneId, Parcel parcel)
        {
            Parcel temp = parcel;
            temp.DroneId = droneId;
            temp.Scheduled = DateTime.Now;

            DataSource.parcels.Remove(parcel);
            DataSource.parcels.Add(temp);
        }
        public static void NewStation(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.stations.Add(new Station
            {
                Id = id,
                Name = name,
                Longitude = longitude,
                Lattitude = longitude,
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
    }

}
