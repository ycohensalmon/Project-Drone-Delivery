using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DataSource
    {
        internal static IDAL.DO.Drone[] drones = new IDAL.DO.Drone[10];
        //internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>(10);
        internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>(5);   
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>(100); 
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>(1000);  
        internal class Config
        {
            internal static int IndexDrone = 0;
            internal static int IndexStation = 0;
            internal static int IndexCustomer = 0;
            internal static int IndexParcel = 0;
         }
        internal static void Initialize()
        {
            for (int i = 0; i < 2; i++)
            {
                drones[i] = new IDAL.DO.Drone {drone = Random()
                IDAL.DO.Drone = DataSource
            }
        }

    }
}
