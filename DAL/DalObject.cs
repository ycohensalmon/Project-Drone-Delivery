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
        List<Drone> getDrones() { return DataSource.drones; }
        List<Station> getStations() { return DataSource.stations; }
        List<Customer> getCustomers() { return DataSource.customers; }
        List<Parcel> getParcels() { return DataSource.parcels; }
    }
    
}
