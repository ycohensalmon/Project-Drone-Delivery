using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        internal static IDAL.DO.Drone[] drones = new IDAL.DO.Drone[10];
        //internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>(10);
        internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>(5);
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>(100);
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>(1000);

        internal static Random rand = new Random();
        internal class Config
        {

            internal static int IndexDrone = 0;
            internal static int IndexStation = 0;
            internal static int IndexCustomer = 0;
            internal static int IndexParcel = 0;
            internal static Random rand = new Random();
        }
        internal static void Initialize()
        {
            InitializeDrone();
            InitializeStation();
            InitializeCustomer();
        }
        public static void InitializeDrone()
        {
            for (int i = 0; i < 2; i++)
            {
                drones[Config.IndexDrone] = new IDAL.DO.Drone()
                {
                    Battery = rand.Next(101),
                    Id = rand.Next(1000, 10001),
                    Model = (IDAL.DO.ModelDrones)rand.Next(3),
                    Status = (IDAL.DO.DroneStatuses)rand.Next(3),
                    MaxWeight = (IDAL.DO.WeightCategory)rand.Next(3)
                };
            }
        }
        public static void InitializeStation()
        {
            stations = new List<IDAL.DO.Station>()
            {
                new IDAL.DO.Station
                {
                    Id = 1010,
                    Name = "Malcha Mall",
                    Lattitude = 31.751716,
                    Longitude = 35.187202,
                    ChargeSolts = rand.Next(10)
                },
                new IDAL.DO.Station
                {
                        Id = 1020,
                        Name = "Hadar Mall",
                        Lattitude = 31.753791,
                        Longitude = 35.213429,
                        ChargeSolts = rand.Next(10)
                },
                    new IDAL.DO.Station
                    {
                        Id = 1030,
                        Name = "Ramot Mall",
                        Lattitude = 31.817627,
                        Longitude = 35.194476,
                        ChargeSolts = rand.Next(10)
                    },
                    new IDAL.DO.Station
                    {
                        Id = 1040,
                        Name = "Jerusalem Central Station",
                        Lattitude = 31.789061,
                        Longitude = 35.203100,
                        ChargeSolts = rand.Next(10)
                    },
                    new IDAL.DO.Station
                    {
                        Id = 1050,
                        Name = "Mamila Mall",
                        Lattitude = 31.777870,
                        Longitude = 35.224982,
                        ChargeSolts = rand.Next(10)
                    }
            };
        }

        public static void InitializeCustomer()
        {
            
        }
    }
}