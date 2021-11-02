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
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        internal class Config
        {
            internal static Random rand = new Random();
            internal static int SerialNum = 1000;
        }

        /// <summary>
        /// initialize the lists
        /// </summary>
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

        /// <summary>
        /// adds to the list of stations 5 stations
        /// </summary>
        public static void InitializeStation()
        {
            stations.Add(new Station
            {
                Id = 1010,
                Name = "Malcha Mall",
                Longitude = Sexagesimal(31.751716,'N'),
                Lattitude = Sexagesimal(35.187202, 'E'),
                ChargeSolts = Config.rand.Next(10)
            });
            stations.Add(new Station
            {
                Id = 1020,
                Name = "Hadar Mall",
                Longitude = Sexagesimal(31.753791, 'N'),
                Lattitude = Sexagesimal(35.213429, 'E'),
                ChargeSolts = Config.rand.Next(10)
            });
            stations.Add(new Station
            {
                Id = 1030,
                Name = "Ramot Mall",
                Longitude = Sexagesimal(31.817627, 'N'),
                Lattitude = Sexagesimal(35.194476, 'E'),
                ChargeSolts = Config.rand.Next(10)
            });
            stations.Add(new Station
            {
                Id = 1040,
                Name = "Jerusalem Central Station",
                Lattitude = Sexagesimal(31.789061, 'N'),
                Longitude = Sexagesimal(35.203100, 'E'),
                ChargeSolts = Config.rand.Next(10)
            });
            stations.Add(new Station
            {
                Id = 1050,
                Name = "Mamila Mall",
                Longitude = Sexagesimal(31.777870, 'N'),
                Lattitude = Sexagesimal(35.224982, 'E'),
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
                customers.Add(new Customer
                {
                    Id = Config.rand.Next(210000000, 340000000),
                    Phone = Config.rand.Next(0500000000, 0590000000),
                    Name = Convert.ToString((Names)Config.rand.Next(10)),
                    Longitude = Sexagesimal((double)Config.rand.Next(31737458, 31807238) / (double)1000000, 'N'),
                    Latittude = Sexagesimal((double)Config.rand.Next(35174572, 35241141) / (double)1000000, 'E')
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