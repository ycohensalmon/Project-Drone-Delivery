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
        //internal static Drone[] drones = new Drone[10];
        internal static List<Drone> drones = new List<Drone>(10);
        internal static List<Station> stations = new List<Station>(5);
        internal static List<Customer> customers = new List<Customer>(100);
        internal static List<Parcel> parcels = new List<Parcel>(1000);

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
                drones = new List<Drone>()
                {
                    new Drone
                    {
                        Battery = rand.Next(101),
                        Id = rand.Next(1000, 10001),
                        Model = (ModelDrones)rand.Next(3),
                        Status = (DroneStatuses)rand.Next(3),
                        MaxWeight = (WeightCategory)rand.Next(3)
                    }   
                };
            }
        }
        public static void InitializeStation()
        {
            stations = new List<Station>()
            {
                new Station
                {
                    Id = 1010,
                    Name = "Malcha Mall",
                    Lattitude = 31.751716,
                    Longitude = 35.187202,
                    ChargeSolts = rand.Next(10)
                },
                new Station
                {
                        Id = 1020,
                        Name = "Hadar Mall",
                        Lattitude = 31.753791,
                        Longitude = 35.213429,
                        ChargeSolts = rand.Next(10)
                },
                    new Station
                    {
                        Id = 1030,
                        Name = "Ramot Mall",
                        Lattitude = 31.817627,
                        Longitude = 35.194476,
                        ChargeSolts = rand.Next(10)
                    },
                    new Station
                    {
                        Id = 1040,
                        Name = "Jerusalem Central Station",
                        Lattitude = 31.789061,
                        Longitude = 35.203100,
                        ChargeSolts = rand.Next(10)
                    },
                    new Station
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