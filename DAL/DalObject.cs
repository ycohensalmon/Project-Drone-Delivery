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
        public static List<Drone> GetDrones() { return DataSource.drones; }
        public static List<Station> GetStations() { return DataSource.stations; }
        public static List<Customer> GetCustomers() { return DataSource.customers; }
        public static List<Parcel> GetParcels() { return DataSource.parcels; }
        public static Drone GetIndexDrone(int index) { return DataSource.drones[index]; }
        public static Station GetIndexStation(int index) { return DataSource.stations[index]; }
        public static Customer GetIndexCustomer(int index) { return DataSource.customers[index]; }
        public static Parcel GetIndexParcel(int index) { return DataSource.parcels[index]; }
    }
    
}
