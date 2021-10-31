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
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

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
                    MaxWeight = (WeightCategory)Config.rand.Next(3),
                    Status = DroneStatuses.Available
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
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = Config.rand.Next(0500000000, 0590000000),
                Name = "Yossef Cohen",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Moshe Levi",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Naor Ben-Lulu",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Sarah Weill",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Noa Botbol",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Regev Shmoulevits",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Yinon Barsheshet",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Rahamim Yifrah",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Dvora Benguigui",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
            customers.Add(new Customer
            {
                Id = Config.rand.Next(210000000, 340000000),
                Phone = 0 + Config.rand.Next(500000000, 590000000),
                Name = "Hananel Batito",
                Longitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                Latittude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
            });
        }
        public static void InitializeParsel()
        {
            DateTime newDate = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                parcels.Add(new Parcel
                {
                    Id = Config.SerialNum++,
                    SenderId = Config.rand.Next(10000,99999),
                    TargetId = Config.rand.Next(10000, 99999),
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = DateTime.MinValue,
                    PickedUp = DateTime.MinValue,
                    Delivered = DateTime.MinValue,
                    Weight = (WeightCategory)Config.rand.Next(3),
                    Priorities = (Priority)Config.rand.Next(3)
                });
            }
        }
    }
}