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

            Drone? drone = (from s in droneRoot.Elements()
                               where int.Parse(s.Element("Id").Value) == id
                               select new Drone()
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
            var droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            return from droneCh in droneChargeList
                   where predicate(droneCh)
                   select droneCh;
        }
        #endregion

        #region station
        public void NewStation(Station station)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            //check if it realy a new station
            if (stationList.Exists(s => s.Id == station.Id))
                throw new ItemAlreadyExistException("station", station.Id);

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
            var stationList = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station station = GetStationById(stationId);
            if (station.Id != stationId)
                throw new IdNotFoundException(stationId, "Station");

            stationList.Remove(station);

            if (newName != "")
                stationList.Add(station);

            if (newChargeSolts != "")
                station.ChargeSolts = result;

            stationList.Add(station);
            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
        }

        public void UpdateDrone(int droneId, string model)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);

            Drone drone = GetDroneById(droneId);
            if (drone.Id != droneId)
                throw new IdNotFoundException(droneId, "Drone");

            var droneNode = (from d in droneRoot.Elements()
                           where d.Element("id").Value == droneId.ToString()
                           select d).FirstOrDefault();

            droneNode.Element("model").SetValue(model);
            XmlTools.SaveListToXMLElement(droneRoot, dronePath);
        }

        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<Customer>(customerPath);

            Customer customer = GetCustomerById(customerID);

            customerList.Remove(customer);

            if (newName != "")
                customer.Name = newName;

            if (newPhone != "")
                customer.Phone = int.Parse(newPhone);

            customerList.Add(customer);
            XmlTools.SaveListToXMLSerializer(customerList, customerPath);
        }



        // to do - Elhanan
        public int NewParcel(Parcel parcel)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            //check if it realy a new parcel
            if (parcelList.Exists(p => p.Id == parcel.Id))
                throw new ItemAlreadyExistException("parcel", parcel.Id);

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            return parcel.Id;
        }

        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            var droneList = XmlTools.LoadListFromXMLSerializer<Drone>(dronePath);

            if (droneList.Exists(d => d.Id == droneId))
                throw new IdNotFoundException(droneId, "Drone");

            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcel = GetParcelById(parcelId);
            parcelList.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }

        public void CollectParcelByDrone(int parcelId)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel parcel = GetParcelById(parcelId);
            parcelList.Remove(parcel);

            parcel.PickedUp = DateTime.Now;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }

        public void DeliveredParcel(int parcelId)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel parcel = GetParcelById(parcelId);
            parcelList.Remove(parcel);

            parcel.Delivered = DateTime.Now;
            parcel.DroneId = 0;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }

        public void SendDroneToBaseCharge(int droneId, int stationId)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            var droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

            Drone drone = GetDroneById(droneId);
            Station station = GetStationById(stationId);
            stationList.Remove(station);

            droneChargeList.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id,
                EnteryTime = DateTime.Now
            });
            station.ChargeSolts--;

            stationList.Add(station);
            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
            XmlTools.SaveListToXMLSerializer(droneChargeList, droneChargePath);
        }

        public double ReleaseDroneFromCharging(int droneId)
        {
            var droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            var stationList = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            DroneCharge droneCharge = droneChargeList.FirstOrDefault(x => x.DroneId == droneId);
            if (droneCharge.DroneId != droneId)
                throw new IdNotFoundException(droneId, "Station charge");

            int stationId = droneCharge.StationId;
            Station station = GetStationById(stationId);
            stationList.Remove(station);

            station.ChargeSolts++;

            stationList.Add(station);
            droneChargeList.Remove(droneCharge);

            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
            XmlTools.SaveListToXMLSerializer(droneChargeList, droneChargePath);

            //note: this return in the second, efter simulator change it to minute
            return (DateTime.Now - droneCharge.EnteryTime).Value.TotalSeconds;
        }

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            return from item in parcelList
                   where predicate(item)
                   select item;
        }

        public Parcel GetParcelById(int id)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel parcel = parcelList.FirstOrDefault(x => x.Id == id);
            if (parcel.Id != id)
                throw new IdNotFoundException(id, "parcel");
            return parcel;
        }

        public double[] PowerConsumptionByDrone()
        {
           //var d = XmlTools.LoadListFromXMLSerializer<double>(configPath).ToArray();
            double[] battery = new double[5];
            battery[0] = 0.2;// DataSource.Config.Available;
            battery[1] = 1;// DataSource.Config.LightParcel;
            battery[2] = 1.5;// DataSource.Config.MediumParcel;
            battery[3] = 2;// DataSource.Config.HeavyParcel;
            battery[4] = 60;// DataSource.Config.LoadingRate;
            return battery;
            //return battery;
        }
    }
}
