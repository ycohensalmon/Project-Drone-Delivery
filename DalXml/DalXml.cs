using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class DalXml : IDal
    {
        #region singelton
        // =null that if we dont need to create a "new bl" it will not create it
        private static DalXml instance = null;
        // for safty. So that if requests come from two places at the same time, it will not create it twice 
        private static readonly object padlock = new object();

        public static DalXml Instance
        {
            get
            {
                //if "instance" hasn`t yet been created, a new one will be created 
                if (instance == null)
                {
                    //stops a request from two places at the same time
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DalXml();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Paths
        readonly string dronePath;
        readonly string droneChargePath;
        readonly string stationsPath;
        readonly string customerPath;
        readonly string parcelPath;
        readonly string userPath;
        readonly string configPath;
        public static string localPath;
        DalXml()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            localPath = Path.GetDirectoryName(str);
            for (int i = 0; i < 4; i++)
                localPath = Path.GetDirectoryName(localPath);

            localPath += @"\Data";

            dronePath = localPath + @"\DroneXml.xml";
            droneChargePath = localPath + @"\DroneChargeXml.xml";
            stationsPath = localPath + @"\StationXml.xml";
            customerPath = localPath + @"\CustomerXml.xml";
            parcelPath = localPath + @"\ParcelXml.xml";
            userPath = localPath + @"\UserXml.xml";
            configPath = localPath + @"\configXml.xml";
        }
        #endregion

        public void NewStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void NewDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void NewCostumer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int NewParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            throw new NotImplementedException();
        }

        public void CollectParcelByDrone(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void DeliveredParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToBaseCharge(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public double ReleaseDroneFromCharging(int droneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(int droneId, string model)
        {
            throw new NotImplementedException();
        }

        public void UpdateBase(int stationId, string newName, string newChargeSolts, int result)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDrones(Func<Drone, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Station GetStationById(int id)
        {
            throw new NotImplementedException();
        }

        public Drone GetDroneById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcelById(int id)
        {
            throw new NotImplementedException();
        }

        public double[] PowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
