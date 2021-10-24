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
        internal static List<Drone> drones = new List<Drone>(10);
        internal static List<Station> stations = new List<Station>(5);
        internal static List<Customer> customers = new List<Customer>(100);
        internal static List<Parcel> parcels = new List<Parcel>(1000);

        internal static Random rand = new Random();
        internal static int randCustomer = rand.Next(210000000, 340000000);
        internal static int randDrone = rand.Next(1000, 10001);
        internal static int 

        //internal class Config
        //{
        //    internal static int IndexDrone = 0;
        //    internal static int IndexStation = 0;
        //    internal static int IndexCustomer = 0;
        //    internal static int IndexParcel = 0;
        //}
        internal static void Initialize()
        {
            InitializeDrone();
            InitializeStation();
            InitializeCustomer();
            InitializeParsel();
        }
        public static void InitializeDrone()
        {
            for (int i = 0; i < 2; i++)
            {
                drones.Add(new Drone
                {
                    Battery = rand.Next(101),
                    Id = randDrone,
                    Model = (ModelDrones)rand.Next(),
                    Status = (DroneStatuses)rand.Next(),
                    MaxWeight = (WeightCategory)rand.Next()
                });
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
            for (int i = 0; i < 10; i++)
            {
                customers.Add(new Customer
                {
                    Id = randCustomer,
                    Phone = rand.Next(0500000000, 0590000000),
                    Name = (Names)rand.Next(),
                    Longitude = (double)rand.Next(31737458, 35174572) / (double)100000,
                    Latittude = (double)rand.Next(31807238, 35241141) / (double)100000
                });
            }
        }
        public static void InitializeParsel()
        {
            DateTime newDate = DateTime.Now;
            parcels = new List<Parcel>()
            {
                new Parcel
                {
                    Id = 
                    SenderId = rand.Next(210000000, 340000000),
                    TargetId = randCustomer,
                    DroneId = randDrone,
                    Requested = newDate,
                    Scheduled = newDate.AddMinutes(rand.Next(15,30)),
                    PickedUp = newDate.AddMinutes(rand.Next(12,40)),
                    Delivered = newDate.AddMinutes(rand.Next(20,45)),
                    Weight = (WeightCategory)rand.Next(3),
                    Priorities = (Priority)rand.Next(3)
                }
            };
        }
    }
}