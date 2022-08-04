using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL : IBL
    {
        #region drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneInList> GetDrones(Predicate<DroneInList> predicate = null)
        {
            if (predicate != null)
            {
                List<DroneInList> droneInLists = drones.FindAll(x => predicate(x));
                return !droneInLists.Any() ? throw new EmptyListException("drones") : droneInLists.Select(x => x);
            }

            return !drones.Any() ? throw new EmptyListException("drones") : drones.Select(x => x);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<string> GetNamesOfAvailableChargeSolts()
        {
            IEnumerable<string> names = from x in GetStationWithChargeSolts()
                                        select x.Name;
            return names.Count() == 0 ? throw new EmptyListException("available chargeSolts") : names.Select(x => x);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Location> GetLocationsDrones()
        {
            IEnumerable<Location> locations = from loc in GetDrones()
                                              select loc.Location;
            return locations;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDroneById(int droneId)
        {
            DroneInList droneInList = drones.FirstOrDefault(x => x.Id == droneId);
            if (droneInList == null)
                throw new ItemNotFoundException(droneId, "Drone");

            ParcelInTravel parcelInTravel = new();

            //filling the "parcelInTravel" method (if this drone was connected to parcel)
            if (droneInList.NumParcel != 0)
            {
                Parcel parcel = GetParcelById(droneInList.NumParcel);

                parcelInTravel.Id = parcel.Id;
                parcelInTravel.Weight = parcel.Weight;
                parcelInTravel.Priorities = parcel.Priorities;
                parcelInTravel.Sender = parcel.Sender;
                parcelInTravel.Target = parcel.Target;
                parcelInTravel.source = GetCustomerById(parcel.Sender.Id).Location;
                parcelInTravel.Destination = GetCustomerById(parcel.Target.Id).Location;

                if (parcel.PickedUp != null)
                {
                    parcelInTravel.InTravel = true;
                    //Getting distance from the drone to target
                    parcelInTravel.Distance = Distance.GetDistanceFromLatLonInKm(
                        droneInList.Location.Latitude, droneInList.Location.Longitude,
                        GetCustomerById(parcel.Target.Id).Location.Latitude, GetCustomerById(parcel.Target.Id).Location.Longitude);
                }
                else
                {
                    parcelInTravel.InTravel = false;
                    //Getting distance from the drone to Sender
                    parcelInTravel.Distance = Distance.GetDistanceFromLatLonInKm(
                        droneInList.Location.Latitude, droneInList.Location.Longitude,
                        GetCustomerById(parcel.Sender.Id).Location.Latitude, GetCustomerById(parcel.Sender.Id).Location.Longitude);
                }
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
        #endregion

        #region customer

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustumerInList> GetCustomers(Predicate<CustumerInList> predicate = null)
        {
            lock (dalObj)
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
                        ParcelsShippedAndDelivered = customer.ParcelsFromCustomer.Count(x => x.Status == ParcelStatuses.Delivered),
                        ParcelsShippedAndNotDelivered = customer.ParcelsFromCustomer.Count(x => x.Status != ParcelStatuses.Delivered),
                        ParcelsHeRecieved = customer.ParcelsToCustomer.Count(x => x.Status == ParcelStatuses.Delivered),
                        ParcelsOnTheWay = customer.ParcelsToCustomer.Count(x => x.Status != ParcelStatuses.Delivered),
                        IsDeleted = item.IsDeleted,
                        
                        Photo = customer.Photo,
                        IsAdmin = customer.IsAdmin,
                        SafePassword = customer.SafePassword
                    });
                }

                if (predicate != null)
                    customers = customers.FindAll(x => predicate(x));

                if (!customers.Any())
                    throw new EmptyListException("customers");

                return customers;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelAtCustomer> GetParcelFromCustomer(int customerId)
        {
            lock (dalObj)
            {
                return from item in dalObj.GetParcels()
                       where item.SenderId == customerId
                       select new ParcelAtCustomer
                       {
                           Id = item.Id,
                           Status = GetParcelStatus(GetParcelById(item.Id)),
                           Weight = (WeightCategory)item.Weight,
                           Priorities = (Priority)item.Priorities,
                           CustomerInParcel = new CustomerInParcel
                           {
                               Id = item.TargetId,
                               Name = dalObj.GetCustomerById(item.TargetId).Name
                           }
                           
                       };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelAtCustomer> GetParcelToCustomer(int customerId)
        {
            lock (dalObj)
            {
                return from item in dalObj.GetParcels()
                       where item.TargetId == customerId
                       select new ParcelAtCustomer
                       {
                           Id = item.Id,
                           Status = GetParcelStatus(GetParcelById(item.Id)),
                           Weight = (WeightCategory)item.Weight,
                           Priorities = (Priority)item.Priorities,
                           CustomerInParcel = new CustomerInParcel
                           {
                               Id = item.SenderId,
                               Name = dalObj.GetCustomerById(item.SenderId).Name
                           }
                       };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<string> GetNamesOfCustomer()
        {
            lock (dalObj)
            {
                IEnumerable<string> names = from x in dalObj.GetCustomers()
                                            select x.Name;
                return names.Count() == 0 ? throw new EmptyListException("customers") : names.Select(x => x);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerById(int customerId)
        {
            lock (dalObj)
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
                    ParcelsToCustomer = GetParcelToCustomer(customerId).ToList(),
                    
                    Photo = customer.Photo,
                    IsAdmin = customer.IsAdmin,
                    SafePassword = customer.SafePassword
                };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetCustomerIdByName(string name)
        {
            lock (dalObj)
            {
                return dalObj.GetCustomers().FirstOrDefault(x => x.Name == name).Id;
            }
        }
        #endregion

        #region parcel

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelInList> GetParcels(Predicate<ParcelInList> predicate = null)
        {
            lock (dalObj)
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
                        Status = GetParcelStatus(parcel),
                        Weight = parcel.Weight,
                        Priorities = parcel.Priorities,
                        IsDeleted = item.IsDeleted
                    });
                }

                if (predicate != null)
                    parcels = parcels.FindAll(x => predicate(x));

                if (!parcels.Any())
                    throw new EmptyListException("parcels");

                return parcels.Select(x => x);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelInList> GetParcelsWithoutDrone()
        {
            lock (dalObj)
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
                        Status = GetParcelStatus(parcel),
                        Weight = parcel.Weight,
                        Priorities = parcel.Priorities,
                        IsDeleted = item.IsDeleted
                    });
                }

                if (!parcels.Any())
                    throw new EmptyListException("parcels without drone");

                return parcels;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelById(int parcelid)
        {
            lock (dalObj)
            {
                DO.Parcel parcel = dalObj.GetParcelById(parcelid);
                CustomerInParcel senderOfParcel = new();
                CustomerInParcel targelOfParcel = new();

                senderOfParcel.Id = dalObj.GetCustomerById(parcel.SenderId).Id; ;
                senderOfParcel.Name = dalObj.GetCustomerById(parcel.SenderId).Name;

                targelOfParcel.Id = dalObj.GetCustomerById(parcel.TargetId).Id;
                targelOfParcel.Name = dalObj.GetCustomerById(parcel.TargetId).Name;

                DroneInParcel droneInParcel = new();
                //if (parcel.DroneId != 0)
                //{
                //    DroneInList droneInList = drones.FirstOrDefault(x => x.Id == parcel.DroneId);
                //    droneInParcel.Id = droneInList.Id;
                //    droneInParcel.Battery = droneInList.Battery;
                //    droneInParcel.Location = droneInList.Location;
                //}

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
        }
        #endregion

        #region station

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationList> GetStations(Predicate<StationList> predicate = null)
        {
            lock (dalObj)
            {
                var stations = from item in dalObj.GetStations()
                               let station = GetStationById(item.Id)
                               select new StationList
                               {
                                   Image = item.Image,
                                   Id = station.Id,
                                   Name = station.Name,
                                   ChargeSoltsAvailable = station.ChargeSolts,
                                   ChargeSoltsBusy = station.DroneCharges.Count(),
                                   IsDeleted = item.IsDeleted
                               };

                if (predicate != null)
                    stations = stations.ToList().FindAll(x => predicate(x));

                if (!stations.Any())
                    throw new EmptyListException("stations");

                return stations;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStationById(int stationId)
        {
            lock (dalObj)
            {
                DO.Station station = dalObj.GetStationById(stationId);

                Location temp = new Location { Latitude = station.Latitude, Longitude = station.Longitude };

                return new Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = temp,
                    ChargeSolts = station.ChargeSolts,
                    DroneCharges = (from item in dalObj.GetDroneCharges()
                                    where item.StationId == station.Id
                                    select new DroneCharge
                                    {
                                        DroneId = item.DroneId,
                                        StationId = item.StationId
                                    }).ToList()
                };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetStationIdByName(string name)
        {
            lock (dalObj)
            {
                int id = dalObj.GetStations().FirstOrDefault(x => x.Name == name).Id;
                return id == 0 ? throw new IncorectInputException("name of the station") : id;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationList> GetStationWithChargeSolts()
        {
            lock (dalObj)
            {
                List<StationList> stations = new();

                foreach (var item in dalObj.GetStations(x => x.ChargeSolts != 0))
                {
                    Station station = GetStationById(item.Id);
                    stations.Add(new StationList
                    {
                        Image = item.Image,
                        Id = station.Id,
                        Name = station.Name,
                        ChargeSoltsAvailable = station.ChargeSolts,
                        ChargeSoltsBusy = station.DroneCharges.Count(),
                        IsDeleted = item.IsDeleted
                    });
                }

                if (!stations.Any())
                    throw new EmptyListException("station with charge solts available");

                return stations;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Location> GetLocationsStation()
        {
            IEnumerable<Location> locations = from loc in dalObj.GetStations()
                                              select new Location
                                              {
                                                  Latitude = loc.Latitude,
                                                  Longitude = loc.Longitude
                                              };
            return locations;
        }
        #endregion


        [MethodImpl(MethodImplOptions.Synchronized)]
        public double getLoadingRate() => LoadingRate;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetBatteryIossAvailable() => Available;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetBatteryIossLightParcel() => LightParcel;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetBatteryIossMediumParcel() => MediumParcel;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetBatteryIossHeavyParcel() => HeavyParcel;
    }
}
