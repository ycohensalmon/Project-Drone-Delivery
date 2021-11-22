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
        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new();
        internal static List<Customer> Customers = new();
        internal static List<Parcel> Parcels = new();
        internal static List<DroneCharge> DroneCharges = new();

        internal class Config
        {
            internal static Random rand = new Random();
            internal static int SerialNum = 1000;
            public double Available { get; set; }
            public double LightParcel { get; set; }
            public double MediumParcel { get; set; }
            public double HeavyParcel { get; set; }
            public double LoadingRate { get; set; } // % per hour
        }

        internal static void Initialize()
        {
            InitializeDrone();
            InitializeStation();
            InitializeCustomer();
            InitializeParsel();
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
                    //Battery = Config.rand.Next(101),
                    Id = Config.rand.Next(1000, 10000),
                    Model = Convert.ToString((ModelDrones)Config.rand.Next(5)),
                    MaxWeight = (WeightCategory)Config.rand.Next(3),
                    //Status = DroneStatuses.Available
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
                ChargeSolts = Config.rand.Next(10)
            });
            Stations.Add(new Station
            {
                Id = 1020,
                Name = "Hadar Mall",
                Latitude = 31.753791,
                Longitude = 35.213429,
                ChargeSolts = Config.rand.Next(10)
            });
            Stations.Add(new Station
            {
                Id = 1030,
                Name = "Ramot Mall",
                Latitude = 31.817627,
                Longitude = 35.194476,
                ChargeSolts = Config.rand.Next(10)
            });
            Stations.Add(new Station
            {
                Id = 1040,
                Name = "Jerusalem Central Station",
                Latitude = 31.789061,
                Longitude = 35.203100,
                ChargeSolts = Config.rand.Next(10)
            });
            Stations.Add(new Station
            {
                Id = 1050,
                Name = "Mamila Mall",
                Latitude = 31.777870,
                Longitude = 35.224982,
                ChargeSolts = Config.rand.Next(10)
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
                    Name = Convert.ToString((Names)Config.rand.Next(10)),
                    Latitude = (double)Config.rand.Next(31737458, 31807238) / (double)1000000,
                    Longitude = (double)Config.rand.Next(35174572, 35241141) / (double)1000000
                });
            }
        }

        /// <summary>
        /// adds 10 parcels to the list of the parcels
        /// </summary>
        public static void InitializeParsel()
        {
            DateTime newDate = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                int senderID = Config.rand.Next(Customers[Customers.Count].Id - 1);

                Parcels.Add(new Parcel
                {
                    Id = Config.SerialNum++,
                    SenderId = senderID,
                    TargetId = GetTargetId(senderID),
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

        private static int GetTargetId(int senderID)
        {
            int targetID = Config.rand.Next(Customers[Customers.Count].Id - 1);
            while (targetID == senderID)
                targetID = Config.rand.Next(Customers[Customers.Count].Id - 1);

            return targetID;
        }

        //bonus methods to display sexasegimal coordination and find distance between ocations
        /// <summary>
        /// finds sexasegiamal value of latitude
        /// </summary>
        /// <param name="decimalDegree">Longitude or latitude</param>
        /// <param name="c">Air directions</param>
        /// <returns>string with the loation</returns>
        public static string Sexagesimal(double decimalDegree, char c)
        {
            // calculate secondes
            double latDegrees = decimalDegree;
            int latSecondes = (int)Math.Round(latDegrees * 60 * 60);
            // calculate gerees
            double latDegreeWithFraction = decimalDegree;
            int degrees = (int)latDegreeWithFraction;
            // claculate minutes
            double fractionalDegree = latDegrees - degrees;
            double minutesWithFraction = 60 * fractionalDegree;
            int minutes = (int)minutesWithFraction;
            // calculate seconde with fraction
            double fractionalMinutes = minutesWithFraction - minutes;
            double secondesWithFraction = 60 * fractionalMinutes;

            return $"{degrees}°{minutes}'{string.Format("{0:F3}", secondesWithFraction)}\"{c}";
        }
    }
}