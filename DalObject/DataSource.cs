using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public class DataSource
    {
        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new();
        internal static List<Customer> Customers = new();
        internal static List<Parcel> Parcels = new();
        internal static List<DroneCharge> DroneCharges = new();
        internal static List<User> Users = new();

        internal class Config
        {
            internal static int SerialNum = 1000;

            internal static Random rand = new Random();
            public static double Available { get => 0.2; }    // 0.2% per KM
            public static double LightParcel { get => 1.0; }  // 1%   per KM
            public static double MediumParcel { get => 1.5; } // 1.5% per KM
            public static double HeavyParcel { get => 2; }    // 2%   per KM
            public static double LoadingRate { get => 60; }   // 40%  per hour
        }

        internal static void Initialize()
        {
            InitializeDrone();
            InitializeStation();
            InitializeCustomer();
            InitializeParsel();
            InitializeUser();
        }

        /// <summary>
        /// initialize 2 user
        /// </summary>
        private static void InitializeUser()
        {
            Users.Add(new User
            {
                Id = 12345678,
                UserName = "Elhanan",
                IsAdmin = true,
                IsDeleted = false,
                SafePassword = "1234",
                Photo = ""
            });

            Users.Add(new User
            {
                Id = 23456789,
                UserName = "Yossef",
                IsAdmin = false,
                IsDeleted = false,
                SafePassword = "4321",
                Photo = ""
            });
        }

        /// <summary>
        /// add to the list of drones 5 drones
        /// </summary>
        public static void InitializeDrone()
        {
            for (int i = 0; i < 5; i++)
            {
                Drones.Add(new Drone
                {
                    Id = Config.rand.Next(1000, 10000),
                    Model = Convert.ToString((ModelDrones)Config.rand.Next(5)),
                    MaxWeight = (WeightCategory)Config.rand.Next(3),
                    IsDeleted = false
                });
            }
        }

        /// <summary>
        /// adds to the list of stations 5 stations
        /// </summary>
        public static void InitializeStation()
        {
            Stations.Add(new Station
            {
                Id = 1010,
                Name = "Malcha Mall",
                Latitude = 31.751716,
                Longitude = 35.187202,
                ChargeSolts = Config.rand.Next(10) + 1,
                IsDeleted = false
            });
            Stations.Add(new Station
            {
                Id = 1020,
                Name = "Hadar Mall",
                Latitude = 31.753791,
                Longitude = 35.213429,
                ChargeSolts = Config.rand.Next(10) + 1,
                IsDeleted = false
            });
            Stations.Add(new Station
            {
                Id = 1030,
                Name = "Ramot Mall",
                Latitude = 31.817627,
                Longitude = 35.194476,
                ChargeSolts = Config.rand.Next(10) + 1,
                IsDeleted = false
            });
            Stations.Add(new Station
            {
                Id = 1040,
                Name = "Jerusalem Central Station",
                Latitude = 31.789061,
                Longitude = 35.203100,
                ChargeSolts = Config.rand.Next(10) + 1,
                IsDeleted = false
            });
            Stations.Add(new Station
            {
                Id = 1050,
                Name = "Mamila Mall",
                Latitude = 31.777870,
                Longitude = 35.224982,
                ChargeSolts = Config.rand.Next(10) + 1,
                IsDeleted = false
            });
        }

        /// <summary>
        /// adds 10 customers to the list of customers
        /// </summary>
        public static void InitializeCustomer()
        {
            for (int i = 0; i < 10; i++)
            {
                Customers.Add(new Customer
                {
                    Id = Config.rand.Next(210000000, 340000000),
                    Phone = Config.rand.Next(0500000000, 0590000000),
                    Name = Convert.ToString((Names)i),
                    Latitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                    Longitude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000,
                    IsDeleted = false
                });
            }
        }

        /// <summary>
        /// adds 10 parcels to the list of the parcels
        /// </summary>
        public static void InitializeParsel()
        {
            DateTime newDate = DateTime.Now;
            int i = 0;
            for (; i < 5; i++)
            {
                int senderID = Customers.ElementAt(Config.rand.Next(Customers.Count)).Id;
                Parcels.Add(new Parcel
                {
                    Id = Config.SerialNum++,
                    SenderId = senderID,
                    TargetId = GetTargetId(senderID),
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null,
                    Weight = (WeightCategory)Config.rand.Next(3),
                    Priorities = (Priority)Config.rand.Next(3)
                });
            }

            for (; i < 10; i++)
            {
                int senderID = Customers.ElementAt(Config.rand.Next(Customers.Count)).Id;
                Parcels.Add(new Parcel
                {
                    Id = Config.SerialNum++,
                    SenderId = senderID,
                    TargetId = GetTargetId(senderID),
                    DroneId = Drones[Config.rand.Next(Drones.Count())].Id,
                    Requested = DateTime.Now.AddMinutes(-(Config.rand.Next(455, 500))),
                    Scheduled = DateTime.Now.AddMinutes(-(Config.rand.Next(442, 450))),
                    PickedUp = DateTime.Now.AddMinutes(-(Config.rand.Next(430, 441))),
                    Delivered = DateTime.Now.AddMinutes(-(Config.rand.Next(415, 429))),
                    Weight = (WeightCategory)Config.rand.Next(3),
                    Priorities = (Priority)Config.rand.Next(3)
                });
            }
        }

        private static int GetTargetId(int senderID)
        {
            int targetID = Customers[Config.rand.Next(0, Customers.Count)].Id;
            while (targetID == senderID)
                targetID = Customers[Config.rand.Next(0, Customers.Count)].Id;

            return targetID;
        }
    }
}