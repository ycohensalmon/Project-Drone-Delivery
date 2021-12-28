using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;

namespace BL
{
    internal partial class BL : IBL
    {
        public Station GetStationById(int stationId)
        {
            DO.Station station = dalObj.GetStationById(stationId);

            Location temp = new Location { Latitude = station.Latitude, Longitude = station.Longitude };

            //fill the List droneChargesBL
            var droneChargesBL = (from item in dalObj.GetDroneCharges()
                                  where item.StationId == station.Id
                                  select new DroneCharge
                                  {
                                      DroneId = item.DroneId,
                                      StationId = item.StationId
                                  }).ToList();

            return new Station
            {
                Id = station.Id,
                Name = station.Name,
                Location = temp,
                ChargeSolts = station.ChargeSolts,
                DroneCharges = droneChargesBL
            };
        }

        public Drone GetDroneById(int droneId)
        {
            DroneInList droneInList = drones.Find(x => x.Id == droneId);
            if (droneInList == null)
                throw new ItemNotFoundException(droneId, "Drone");

            ParcelInTravel parcelInTravel = new();

            //filling the "parcelInTravel" method (if this drone was connected to parcel)
            if (droneInList.NumParcel != 0)
            {
                Parcel parcel = GetParcelById(droneInList.NumParcel);

                parcelInTravel.Id = parcel.Id;
                if (parcel.PickedUp != null)
                    parcelInTravel.InTravel = true;
                else
                    parcelInTravel.InTravel = false;
                parcelInTravel.Weight = parcel.Weight;
                parcelInTravel.Priorities = parcel.Priorities;
                parcelInTravel.Sender = parcel.Sender;
                parcelInTravel.Target = parcel.Target;
                parcelInTravel.source = GetCustomerById(parcel.Sender.Id).Location;
                parcelInTravel.Destination = GetCustomerById(parcel.Target.Id).Location;
                //Getting distance from the drone to target
                parcelInTravel.Distance = Distance.GetDistanceFromLatLonInKm(
                    droneInList.Location.Latitude, droneInList.Location.Longitude,
                    GetCustomerById(parcel.Sender.Id).Location.Latitude, GetCustomerById(parcel.Sender.Id).Location.Longitude);
            }

            return new Drone
            {
                Id = droneInList.Id,
                Model = droneInList.Model,
                Battery = droneInList.Battery,
                Location = droneInList.Location,
                MaxWeight = droneInList.MaxWeight,
                Status = droneInList.Status,
                ParcelInTravel = parcelInTravel
            };
        }


        public Customer GetCustomerById(int customerId)
        {
            //find the Customer and his parcels fron DL
            DO.Customer customer = dalObj.GetCustomerById(customerId);

            Location temp = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };

            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = temp,
                ParcelsFromCustomer = GetParcelFromCustomer(customerId).ToList(),
                ParcelsToCustomer = GetParcelToCustomer(customerId).ToList()
            };
        }

        public IEnumerable<ParcelAtCustomer> GetParcelFromCustomer(int customerId)
        {
            return from item in dalObj.GetParcels()
                   where item.SenderId == customerId
                   select new ParcelAtCustomer
                   {
                       Id = item.Id,
                       Requested = item.Requested,
                       Scheduled = item.Scheduled,
                       PickedUp = item.PickedUp,
                       Delivered = item.Delivered,
                       Weight = (WeightCategory)item.Weight,
                       Priorities = (Priority)item.Priorities,
                       CustomerInParcel = new CustomerInParcel
                       {
                           Id = item.TargetId,
                           Name = dalObj.GetCustomerById(item.TargetId).Name
                       }
                   };
        }

        public IEnumerable<ParcelAtCustomer> GetParcelToCustomer(int customerId)
        {
            return from item in dalObj.GetParcels()
                   where item.TargetId == customerId
                   select new ParcelAtCustomer
                   {
                       Id = item.Id,
                       Requested = item.Requested,
                       Scheduled = item.Scheduled,
                       PickedUp = item.PickedUp,
                       Delivered = item.Delivered,
                       Weight = (WeightCategory)item.Weight,
                       Priorities = (Priority)item.Priorities,
                       CustomerInParcel = new CustomerInParcel
                       {
                           Id = item.SenderId,
                           Name = dalObj.GetCustomerById(item.SenderId).Name
                       }
                   };
        }

        public Parcel GetParcelById(int parcelid)
        {
            DO.Parcel parcel = dalObj.GetParcelById(parcelid);
            CustomerInParcel senderOfParcel = new();
            CustomerInParcel targelOfParcel = new();

            senderOfParcel.Id = dalObj.GetCustomerById(parcel.SenderId).Id; ;
            senderOfParcel.Name = dalObj.GetCustomerById(parcel.SenderId).Name;

            targelOfParcel.Id = dalObj.GetCustomerById(parcel.TargetId).Id;
            targelOfParcel.Name = dalObj.GetCustomerById(parcel.TargetId).Name;

            DroneInParcel droneInParcel = new();
            if (parcel.DroneId != 0)
            {
                DroneInList droneInList = drones.FirstOrDefault(x => x.Id == parcel.DroneId);
                droneInParcel.Id = droneInList.Id;
                droneInParcel.Battery = droneInList.Battery;
                droneInParcel.Location = droneInList.Location;
            }

            return new Parcel
            {
                Id = parcel.Id,
                Sender = senderOfParcel,
                Target = targelOfParcel,
                Drone = droneInParcel,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered,
                Weight = (WeightCategory)parcel.Weight,
                Priorities = (Priority)parcel.Priorities
            };
        }

        public User GetUserById(int userId)
        {
            DO.User user = dalObj.GetUserById(userId);

            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
                SafePassword = user.SafePassword,
                Photo = user.Photo,
                customer = GetCustomerById(userId)
            };
        }

        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null)
        {
            List<StationList> stations = new();

            foreach (var item in dalObj.GetStations())
            {
                Station station = GetStationById(item.Id);
                stations.Add(new StationList
                {
                    Id = station.Id,
                    Name = station.Name,
                    ChargeSoltsAvailable = station.ChargeSolts,
                    ChargeSoltsBusy = station.DroneCharges.Count()
                });
            }

            if (predicate != null)
                stations = stations.FindAll(x => predicate(x));

            if (!stations.Any()) 
                throw new EmptyListException("stations");

            return stations;
        }

        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null)
        {
            if (predicate != null)
            {
                List<DroneInList> droneInLists = drones.FindAll(x => predicate(x));
                return !droneInLists.Any() ? throw new EmptyListException("drones") : droneInLists.Select(x => x);
            }

            return !drones.Any() ? throw new EmptyListException("drones") : drones.Select(x => x);
        }

        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null)
        {
            List<CustumerInList> customers = new();

            foreach (var item in dalObj.GetCustomers())
            {
                Customer customer = GetCustomerById(item.Id);

                customers.Add(new CustumerInList
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    ParcelsShippedAndDelivered = customer.ParcelsFromCustomer.Count(x => x.Delivered != null),
                    ParcelsShippedAndNotDelivered = customer.ParcelsFromCustomer.Count(x => x.Delivered == null),
                    ParcelsHeRecieved = customer.ParcelsToCustomer.Count(x => x.Delivered != null),
                    ParcelsOnTheWay = customer.ParcelsToCustomer.Count(x => x.Delivered == null)
                });
            }

            if (predicate != null)
                customers = customers.FindAll(x => predicate(x));

            if (!customers.Any())
                throw new EmptyListException("customers");

            return customers;
        }

        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null)
        {
            List<ParcelInList> parcels = new();

            foreach (var item in dalObj.GetParcels())
            {
                Parcel parcel = GetParcelById(item.Id);

                parcels.Add(new ParcelInList
                {
                    Id = parcel.Id,
                    SenderName = parcel.Sender.Name,
                    TargetName = parcel.Target.Name,
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    PickedUp = parcel.PickedUp,
                    Delivered = parcel.Delivered,
                    Weight = parcel.Weight,
                    Priorities = parcel.Priorities
                });
            }

            if (predicate != null)
                parcels = parcels.FindAll(x => predicate(x));

            if (!parcels.Any())
                throw new EmptyListException("parcels");

            return parcels.Select(x => x);
        }

        public IEnumerable<string> GetNamesOfCustomer()
        {
            IEnumerable<string> names = from x in dalObj.GetCustomers()
                                        select x.Name;
            return (IEnumerable<string>)names.Select(x=>x);
        }

        public IEnumerable<string> GetNamesOfAvailableChargeSolts()
        {
            IEnumerable<string> names = from x in GetStationWithChargeSolts()
                                        select x.Name;
            return names;
        }

        public int GetStationIdByName(string name)
        {
            return dalObj.GetStations().FirstOrDefault(x => x.Name == name).Id;
        }

        public int GetCustomerIdByName(string name)
        {
            return dalObj.GetCustomers().FirstOrDefault(x => x.Name == name).Id;
        }

        public IEnumerable<ParcelInList> GetParcelsWithoutDrone()
        {
            List<ParcelInList> parcels = new();

            foreach (var item in dalObj.GetParcels(x => x.DroneId == 0))
            {
                Parcel parcel = GetParcelById(item.Id);

                parcels.Add(new ParcelInList
                {
                    Id = parcel.Id,
                    SenderName = parcel.Sender.Name,
                    TargetName = parcel.Target.Name,
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    Delivered = parcel.Delivered,
                    Weight = parcel.Weight,
                    Priorities = parcel.Priorities
                });
            }

            if (!parcels.Any())
                throw new EmptyListException("parcels without drone");

            return parcels;
        }

        public IEnumerable<StationList> GetStationWithChargeSolts()
        {
            List<StationList> stations = new();

            foreach (var item in dalObj.GetStations(x => x.ChargeSolts != 0))
            {
                Station station = GetStationById(item.Id);
                stations.Add(new StationList
                {
                    Id = station.Id,
                    Name = station.Name,
                    ChargeSoltsAvailable = station.ChargeSolts,
                    ChargeSoltsBusy = station.DroneCharges.Count()
                });
            }

            if (!stations.Any())
                throw new EmptyListException("station with charge solts available");

            return stations;

        }
    }
}
