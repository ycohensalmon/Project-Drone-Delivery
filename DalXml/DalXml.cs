using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        readonly string batteryPath;
        public static string localPath;

        DalXml()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            localPath = Path.GetDirectoryName(str);
            localPath = Path.GetDirectoryName(localPath);
            localPath = Path.GetDirectoryName(localPath);

            localPath += @"\Data";

            dronePath = localPath + @"\DroneXml.xml";
            droneChargePath = localPath + @"\DroneChargeXml.xml";
            stationsPath = localPath + @"\StationXml.xml";
            customerPath = localPath + @"\CustomerXml.xml";
            parcelPath = localPath + @"\ParcelXml.xml";
            userPath = localPath + @"\UserXml.xml";
            configPath = localPath + @"\configXml.xml";
            batteryPath = localPath + @"\BattryXml.xml";

            var droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            foreach (var drone in droneCharge)
                ReleaseDroneFromCharging(drone.DroneId);
            droneCharge.Clear();
            XmlTools.SaveListToXMLSerializer(droneCharge, droneChargePath);
        }
    
        #endregion

        #region drone in XML Element

        [MethodImpl(MethodImplOptions.Synchronized)]
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
                           MaxWeight = (WeightCategory)Enum.Parse(typeof(WeightCategory), s.Element("MaxWeight").Value),
                           IsDeleted = bool.Parse(s.Element("IsDeleted").Value),
                           Image = s.Element("Image").Value
                       };
            }
            catch { return null; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDroneById(int id)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);
            
            Drone? drone = (from s in droneRoot.Elements()
                            where int.Parse(s.Element("Id").Value) == id
                            select new Drone()
                            {
                                Id = int.Parse(s.Element("Id").Value),
                                Model = s.Element("Model").Value,
                                MaxWeight = (WeightCategory)Enum.Parse(typeof(WeightCategory), s.Element("MaxWeight").Value),
                                IsDeleted = bool.Parse(s.Element("IsDeleted").Value),
                                Image = s.Element("Image").Value
                            }).FirstOrDefault();

            if (drone != null)
                return (Drone)drone;
            else
                throw new ItemNotFoundException("drone");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void NewDrone(Drone drone)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);
            droneRoot.Add(createDrone(drone));
            XmlTools.SaveListToXMLElement(droneRoot, dronePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        XElement createDrone(Drone drone)
        {
            return new XElement("Drone",
                    new XElement("Id", drone.Id),
                    new XElement("Model", drone.Model),
                    new XElement("MaxWeight", drone.MaxWeight),
                    new XElement("IsDeleted", drone.IsDeleted),
                    new XElement("Image", drone.Image));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model)
        {
            XElement droneRoot = XmlTools.LoadListFromXMLElement(dronePath);

            Drone drone = GetDroneById(droneId);
            if (drone.Id != droneId)
                throw new IdNotFoundException(droneId, "Drone");

            var droneNode = (from d in droneRoot.Elements()
                             where int.Parse(d.Element("Id").Value) == droneId
                             select d).FirstOrDefault();

            droneNode.Element("Model").SetValue(model);
            XmlTools.SaveListToXMLElement(droneRoot, dronePath);
        }
        #endregion

        #region updateDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int droneId)
        {
            var droneList = XmlTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            Drone drone = GetDroneById(droneId);

            if (drone.Id != droneId)
                throw new IdNotFoundException(droneId, "Drone");
            droneList.Remove(drone);

            drone.IsDeleted = false;

            droneList.Add(drone);
            XmlTools.SaveListToXMLSerializer(droneList, dronePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            if (!GetDrones().Any(d => d.Id == droneId))
                throw new IdNotFoundException(droneId, "Drone");

            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcel = GetParcelById(parcelId);
            parcelList.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectParcelByDrone(int parcelId)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel parcel = GetParcelById(parcelId);
            parcelList.Remove(parcel);

            parcel.PickedUp = DateTime.Now;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        #endregion

        #region drone charge

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null)
        {
            var droneChargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            return from droneCh in droneChargeList
                   where predicate == null ? true : predicate(droneCh)
                   select droneCh;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ClearDroneCharge()
        {
            List<DroneCharge> droneCharge = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            foreach (var item in droneCharge) ReleaseDroneFromCharging(item.DroneId);

            droneCharge.Clear();
            XmlTools.SaveListToXMLSerializer(droneCharge, droneChargePath);
        }
        #endregion

        #region station

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void NewStation(Station station)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            //check if it realy a new station
            if (stationList.Exists(s => s.Id == station.Id))
                throw new ItemAlreadyExistException("station", station.Id);

            stationList.Add(station);
            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);

            return from station in stationList
                   where predicate == null ? true : predicate(station)
                   select station;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStationById(int id)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);

            var station = stationList.FirstOrDefault(s => s.Id == id);
            if (station.Id == id)
                return station;
            else
                throw new DO.ItemNotFoundException("Station");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int stationId)
        {
            var stationList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationsPath);
            Station station = GetStationById(stationId);
            if (station.Id != stationId)
                throw new IdNotFoundException(stationId, "Station");
            stationList.Remove(station);

            station.IsDeleted = true;

            stationList.Add(station);
            XmlTools.SaveListToXMLSerializer(stationList, stationsPath);
        }
        #endregion

        #region customer

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void NewCostumer(Customer customer)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<Customer>(customerPath);

            if (customerList.Exists(x => x.Id == customer.Id)/*&& !customer.IsDeleted*/)
                throw new ItemAlreadyExistException("customer", customer.Id);

            customerList.Add(customer);
            XmlTools.SaveListToXMLSerializer(customerList, customerPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);
            return from customer in customerList
                   where predicate == null ? true : predicate(customer)
                   select customer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerById(int id)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);

            var customer = customerList.FirstOrDefault(s => s.Id == id);
            if (customer.Id == id)
                return customer;
            else
                throw new DO.ItemNotFoundException("customer");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            var customerList = XmlTools.LoadListFromXMLSerializer<DO.Customer>(customerPath);
            Customer customer = GetCustomerById(customerId);

            if (customer.Id != customerId)
                throw new IdNotFoundException(customerId, "Customer");
            customerList.Remove(customer);

            customer.IsDeleted = false;

            customerList.Add(customer);
            XmlTools.SaveListToXMLSerializer(customerList, customerPath);
        }
        #endregion

        #region parcel

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int NewParcel(Parcel parcel)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            //check if it realy a new parcel
            if (parcelList.Exists(p => p.Id == parcel.Id))
                throw new ItemAlreadyExistException("parcel", parcel.Id);

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            return getSerialNum();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            return parcelList.FindAll(x => predicate == null ? true : predicate(x));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelById(int id)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);

            Parcel parcel = parcelList.FirstOrDefault(x => x.Id == id);
            if (parcel.Id != id)
                throw new IdNotFoundException(id, "parcel");
            return parcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int parcelId)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);
            Parcel parcel = GetParcelById(parcelId);

            if (parcel.Id != parcelId)
                throw new IdNotFoundException(parcelId, "Parcel");
            parcelList.Remove(parcel);

            parcel.IsDeleted = false;

            parcelList.Add(parcel);
            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }
        #endregion

        #region tools

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] PowerConsumptionByDrone()
        {
            var getBatteries = XmlTools.LoadListFromXMLSerializer<XmlTools.Battery>(batteryPath).ToArray();

            double[] battery = new double[5];
            battery[0] = (from index in getBatteries
                          where index.Statuse == "Available"
                          select index.lossBattery).FirstOrDefault();
                        
            battery[1] = (from index in getBatteries
                          where index.Statuse == "LightParcel"
                          select index.lossBattery).FirstOrDefault();

            battery[2] = (from index in getBatteries
                          where index.Statuse == "MediumParcel"
                          select index.lossBattery).FirstOrDefault();

            battery[3] = (from index in getBatteries
                          where index.Statuse == "HeavyParcel"
                          select index.lossBattery).FirstOrDefault();

            battery[4] = (from index in getBatteries
                          where index.Statuse == "LoadingRate"
                          select index.lossBattery).FirstOrDefault();

            return battery;
        }

        private int getSerialNum()
        {
            XElement serialNum = XmlTools.LoadListFromXMLElement(configPath);
            int num = int.Parse(serialNum.Element("SerialNum").Value) + 1;
            serialNum.Element("SerialNum").SetValue(num.ToString());
            XmlTools.SaveListToXMLElement(serialNum, configPath);
            return num;
        }
        #endregion
    }
}
