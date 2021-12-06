using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDAL.IDal
    {
        /// <summary>
        /// constructor for DalObject class
        /// </summary>
        public DalObject() => DataSource.Initialize();

        //-----------------------------------------------------------------------------------------------------------//
        // news functions //
        //-----------------------------------------------------------------------------------------------------------//

        public void NewStation(Station station)
        {
            foreach (var item in GetStations())
            {
                if (item.Id == station.Id)
                    throw new IdAlreadyExistException(station.Id,"Station");
            }
            DataSource.Stations.Add(station);

        }

        public void NewDrone(Drone drone)
        {
            foreach (var item in GetDrones())
            {
                if (item.Id == drone.Id)
                    throw new IdAlreadyExistException(drone.Id, "Drone");
            }
            DataSource.Drones.Add(drone);
        }

        public void NewCostumer(Customer customer)
        {
            foreach (var item in GetCustomers())
            {
                if (item.Id == customer.Id)
                    throw new IdAlreadyExistException(customer.Id, "Customer");
            }
            DataSource.Customers.Add(customer);
        }

        public void NewParcel(Parcel parcel) => DataSource.Parcels.Add(parcel);


        //-----------------------------------------------------------------------------------------------------------//
        // uptades fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        public void ConnectDroneToParcel(int droneId, int parcelId)
        {
            if (GetDrones().FirstOrDefault(drone=> drone.Id == droneId).Id != droneId)
                throw new IdNotFoundException(droneId, "Drone");

            Parcel parcel = GetParcelById(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;

            DataSource.Parcels.Add(parcel);
        }
        
        public void CollectParcelByDrone(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.PickedUp = DateTime.Now;

            DataSource.Parcels.Add(parcel);
        }
        
        public void DeliveredParcel(int parcelId)
        {
            Parcel parcel = GetParcelById(parcelId);
            DataSource.Parcels.Remove(parcel);

            parcel.Delivered = DateTime.Now;
            parcel.DroneId = 0;

            DataSource.Parcels.Add(parcel);
        }
        
        public void SendDroneToBaseCharge(int droneId, int stationId)
        {
            Drone drone = GetDroneById(droneId);
            Station station = GetStationById(stationId);
            DataSource.Stations.Remove(station);

            DataSource.DroneCharges.Add(new DroneCharge
            {
                DroneId = drone.Id,
                StationId = station.Id
            });
            station.ChargeSolts--;

            DataSource.Stations.Add(station);
        }

        public void ReleaseDroneFromCharging(int droneId)
        {
            DroneCharge droneCharge = DataSource.DroneCharges.FirstOrDefault(x => x.DroneId == droneId);
            if (droneCharge.DroneId != droneId)
                throw new IdNotFoundException(droneId, "Station charge");

            int stationId = droneCharge.StationId;
            Station station = GetStationById(stationId);
            DataSource.Stations.Remove(station);

            station.ChargeSolts++;

            DataSource.Stations.Add(station);
            DataSource.DroneCharges.Remove(droneCharge);
        }

        public void UpdateDrone(int droneId, string model)
        {
            Drone drone = GetDroneById(droneId);
            if (drone.Id != droneId)
                throw new IdNotFoundException(droneId, "Drone");

            DataSource.Drones.Remove(drone);

            drone.Model = model;

            DataSource.Drones.Add(drone);
        }

        public void UpdateBase(int stationId, string newName, string newChargeSolts, int result)
        {
            Station station = GetStationById(stationId);
            if (station.Id != stationId)
                throw new IdNotFoundException(stationId, "Station");

            if (newName != "") 
            {
                DataSource.Stations.Remove(station);
                station.Name = newName;
                DataSource.Stations.Add(station);
            }

            if (newChargeSolts != "") 
            {
                DataSource.Stations.Remove(station);
                station.ChargeSolts = result;
                DataSource.Stations.Add(station);
            }
        }

        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            Customer customer = GetCustomerById(customerID);

            if (newName != "")
            {
                DataSource.Customers.Remove(customer);
                customer.Name = newName;
                DataSource.Customers.Add(customer);
            }

            if (newPhone != "")
            {
                DataSource.Customers.Remove(customer);
                customer.Phone = Int32.Parse(newPhone);
                DataSource.Customers.Add(customer);
            }
        }

        //-----------------------------------------------------------------------------------------------------------//
        // Get List //
        //-----------------------------------------------------------------------------------------------------------//

        public IEnumerable<Drone> GetDrones(Func<Drone, bool> predicate = null)
            => predicate == null ? DataSource.Drones : DataSource.Drones.Where(predicate);

        public IEnumerable<Station> GetStations(Func<Station, bool> predicate = null)
            => predicate == null ? DataSource.Stations : DataSource.Stations.Where(predicate);

        public IEnumerable<Customer> GetCustomers(Func<Customer, bool> predicate = null)
            => predicate == null ? DataSource.Customers : DataSource.Customers.Where(predicate);

        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate = null)
            => predicate == null ? DataSource.Parcels : DataSource.Parcels.Where(predicate);

        public IEnumerable<DroneCharge> GetDroneCharges(Func<DroneCharge, bool> predicate = null)
            => predicate == null ? DataSource.DroneCharges : DataSource.DroneCharges.Where(predicate);

        //-----------------------------------------------------------------------------------------------------------//
        // get objects by id //
        //-----------------------------------------------------------------------------------------------------------//

        public Station GetStationById(int id)
        {
            Station station = DataSource.Stations.Find(x => x.Id == id);
            if (station.Id != id)
                throw new IdNotFoundException(id, "Station");
            return station;
        }

        public Drone GetDroneById(int id)
        {
            Drone drone = DataSource.Drones.Find(x => x.Id == id);
            if (drone.Id != id)
                throw new IdNotFoundException(id, "Drone");
            return drone;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = DataSource.Customers.Find(x => x.Id == id);
            if (customer.Id != id)
                throw new IdNotFoundException(id, "Customer");
            return customer;
        }

        public Parcel GetParcelById(int id)
        {
            Parcel parcel = DataSource.Parcels.Find(x => x.Id == id);
            if (parcel.Id != id)
                throw new IdNotFoundException(id, "parcel");
            return parcel;
        }

        //-----------------------------------------------------------------------------------------------------------//
        // other //
        //-----------------------------------------------------------------------------------------------------------//
        public double[] PowerConsumptionByDrone()
        {
            double[] battery = new double[5];
            battery[0] = DataSource.Config.Available;
            battery[1] = DataSource.Config.LightParcel;
            battery[2] = DataSource.Config.MediumParcel;
            battery[3] = DataSource.Config.HeavyParcel;
            battery[4] = DataSource.Config.LoadingRate;
            return battery;
        }
    }
}
