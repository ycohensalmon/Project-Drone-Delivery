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
        public DalObject() { DataSource.Initialize(); }
        public static List<Drone> GetDrones() => DataSource.drones;
        public static List<Station> GetStations() => DataSource.stations; 
        public static List<Customer> GetCustomers() => DataSource.customers; 
        public static List<Parcel> GetParcels() => DataSource.parcels;
        public static Drone GetIndexDrone(int index) => DataSource.drones[index];
        public static Station GetIndexStation(int index) => DataSource.stations[index];
        public static Customer GetIndexCustomer(int index) => DataSource.customers[index];
        public static Parcel GetIndexParcel(int index) => DataSource.parcels[index];
        public static void ConnectDroneToParcel(int droneId, int parcelId)
        {
            DalObject.
        }
    }
    
}
