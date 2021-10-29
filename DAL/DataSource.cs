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
        public DataSource() => Initialize();

        internal static List<Drone> drones = new List<Drone>(10);
        internal static List<Station> stations = new List<Station>(5);
        internal static List<Customer> customers = new List<Customer>(100);
        internal static List<Parcel> parcels = new List<Parcel>(1000);

        internal class Config
        {
            internal static Random rand = new Random();
            internal static int SerialNum = 1000;
        }
        internal static void Initialize()
        {
            InitializeDrone();
            InitializeStation();
            InitializeCustomer();
            InitializeParsel();
        }
        public static void InitializeDrone()
        {
            for (int i = 0; i < 5; i++)
            {
                drones.Add(new Drone
                {
                    Battery = Config.rand.Next(101),
                    Id = Config.rand.Next(1000, 10000),
                    Model = (ModelDrones)Config.rand.Next(5),
                    Status = (DroneStatuses)Config.rand.Next(2),
                    MaxWeight = (WeightCategory)Config.rand.Next(3)
                });
            }
        }
        public static void InitializeStation()
        {
            stations.Add(new Station
            {
                Id = 1010,
                Name = "Malcha Mall",
                Lattitude = 31.751716,
                Longitude = 35.187202,
                ChargeSolts = Config.rand.Next(10)
            });

            stations.Add(new Station
            {
                Id = 1020,
                Name = "Hadar Mall",
                Lattitude = 31.753791,
                Longitude = 35.213429,
                ChargeSolts = Config.rand.Next(10)
            });

            stations.Add(new Station
            {
                Id = 1030,
                Name = "Ramot Mall",
                Lattitude = 31.817627,
                Longitude = 35.194476,
                ChargeSolts = Config.rand.Next(10)
            });

            stations.Add(new Station
            {
                Id = 1040,
                Name = "Jerusalem Central Station",
                Lattitude = 31.789061,
                Longitude = 35.203100,
                ChargeSolts = Config.rand.Next(10)
            });

            stations.Add(new Station
            {
                Id = 1050,
                Name = "Mamila Mall",
                Lattitude = 31.777870,
                Longitude = 35.224982,
                ChargeSolts = Config.rand.Next(10)
            });
        }
        public static void InitializeCustomer()
        {
            for (int i = 0; i < 10; i++)
            {
                customers.Add( new Customer
                {
                    Id = Config.rand.Next(210000000, 340000000),
                    Phone = 0 + Config.rand.Next(500000000, 590000000),
                    Name = (Names)Config.rand.Next(8),
                    Longitude = (double)Config.rand.Next(31737458, 35174572) / (double)100000,
                    Latittude = (double)Config.rand.Next(31807238, 35241141) / (double)100000
                });
            }
        }
        public static void InitializeParsel()
        {
            DateTime newDate = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                parcels.Add(new Parcel
                {
                    Id = Config.SerialNum++,
                    SenderId = 0,
                    TargetId = 0,
                    DroneId = 0,
                    Requested = newDate,
                    Scheduled = newDate.AddMinutes(Config.rand.Next(15, 30)),
                    PickedUp = newDate.AddMinutes(Config.rand.Next(12, 40)),
                    Delivered = newDate.AddMinutes(Config.rand.Next(20, 45)),
                    Weight = (WeightCategory)Config.rand.Next(3),
                    Priorities = (Priority)Config.rand.Next(3)
                });
            }
        }
    }
}