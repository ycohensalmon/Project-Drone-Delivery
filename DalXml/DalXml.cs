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

        #region drone in XML Element
        public IEnumerable<Drone> GetDrones()
        {
            try
            {
                XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);

                return from s in droneRoot.Elements()
                       select new Drone()
                       {
                           Id = int.Parse(s.Element("Id").Value),
                           Model = s.Element("Model").Value,
                           MaxWeight = (WeightCategory)Enum.Parse(typeof(WeightCategory), s.Element("MaxWeight").Value)
                       };
            }
            catch { return null; }
        }
        public Drone GetDroneById(int id)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);

            DO.Drone? drone = (from s in droneRoot.Elements()
                               where int.Parse(s.Element("Id").Value) == id
                               select new DO.Drone()
                               {
                                   Id = int.Parse(s.Element("Id").Value),
                                   Model = s.Element("Model").Value,
                                   MaxWeight = (WeightCategory)Enum.Parse(typeof(WeightCategory), s.Element("MaxWeight").Value)
                               }).FirstOrDefault();

            if (drone != null)
                return (Drone)drone;
            else
                throw new DO.ItemNotFoundException("drone");
        }
        public void NewDrone(Drone drone)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);
            droneRoot.Add(createDrone(drone));
            XmlTools.SaveListToXMLElement(droneRoot, dronePath);
        }
        XElement createDrone(Drone drone)
        {
            return new XElement("drone",
                    new XElement("id", drone.Id),
                    new XElement("model", drone.Model),
                    new XElement("maxWeight", drone.MaxWeight));
        }
        #endregion

        #region drone charge
        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null)
        {
            var droneChargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);
            return from droneCh in droneChargeList
                   where predicate(droneCh)
                   select droneCh;
        }
        #endregion

        #region station
        public void NewStation(Station station)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            var st = stationList.FirstOrDefault(s => s.Id == station.Id);
            if (st.Id != station.Id /*&& !station.IsDeleted*/)
                throw new DO.ItemAlreadyExistException("station", station.Id);

            stationList.Add(station);
            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
        }

        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            return from station in stationList
                   where predicate(station)
                   select station;
        }

        public Station GetStationById(int id)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);

            var station = stationList.FirstOrDefault(s => s.Id == id);
            if (station.Id == id)
                return station;
            else
                throw new DO.ItemNotFoundException("Station");
        }
        #endregion

        #region customer
        public void NewCostumer(Customer customer)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);
            var st = customerList.FirstOrDefault(s => s.Id == customer.Id);
            if (st.Id != customer.Id /*&& !customer.IsDeleted*/)
                throw new DO.ItemAlreadyExistException("customer", customer.Id);

            customerList.Add(customer);
            XmlTools.SaveListToXMLSerializer(customerList, customerPath);
        }

        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);
            return from customer in customerList
                   where predicate(customer)
                   select customer;
        }

        public Customer GetCustomerById(int id)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);

            var customer = customerList.FirstOrDefault(s => s.Id == id);
            if (customer.Id == id)
                return customer;
            else
                throw new DO.ItemNotFoundException("customer");
        }
        #endregion

        #region user
        public User GetUserById(int id)
        {
            var userList = XmlTools.LoadListFromXMLSerializer<DO.User>(userPath);

            var user = userList.FirstOrDefault(s => s.Id == id);
            if (user.Id == id)
                return user;
            else
                throw new DO.ItemNotFoundException("user");
        }
        #endregion

        // to do - Yossef
        public void UpdateBase(int stationId, string newName, string newChargeSolts, int result)
        {
            throw new NotImplementedException();
        }
        public void UpdateDrone(int droneId, string model)
        {
            throw new NotImplementedException();
        }
        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            throw new NotImplementedException();
        }



        // to do - Elhanan
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

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
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
    }
}
