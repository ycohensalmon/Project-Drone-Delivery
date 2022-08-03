﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace PO
{
    public class ConvertFonctions
    {
        #region enums BO to PO
        internal static WeightCategory BOEnumWeightCategoryToPO(BO.WeightCategory weight)
        {
            return (WeightCategory)weight;
        }
        internal static DroneStatuses BOEnumDroneStatusesToPO(BO.DroneStatuses status)
        {
            return (DroneStatuses)status;
        }

        internal static Priority BOEnumPriorityToPO(BO.Priority prioruty)
        {
            return (Priority)prioruty;
        }

        internal static ParcelStatuses BOEnumParcelStatusToPO(BO.ParcelStatuses parcelStatuses)
        {
            return (ParcelStatuses)parcelStatuses;
        }
        #endregion

        #region enums PO to BO
        internal static BO.WeightCategory POEnumWeightCategoryToBO(WeightCategory weight)
        {
            return (BO.WeightCategory)weight;
        }

        internal static BO.DroneStatuses POEnumDroneStatusesToBO(DroneStatuses status)
        {
            return (BO.DroneStatuses)status;
        }


        internal static BO.Priority POEnumPriorityToBO(Priority prioruty)
        {
            return (BO.Priority)prioruty;
        }
        internal static BO.ParcelStatuses POEnumParcelStatusToBO(ParcelStatuses parcelStatuses)
        {
            return (BO.ParcelStatuses)parcelStatuses;
        }
        #endregion

        internal static Location BOLocationToPO(BO.Location location)
        {
            return new Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        internal static BO.Location POLocationToBO(Location location)
        {
            return new BO.Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        internal static DroneInParcel BODroneParceToPO(BO.DroneInParcel droneParcel)
        {
            return new DroneInParcel
            {
                Id = droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = BOLocationToPO(droneParcel.Location)
            };
        }

        internal static BO.DroneInParcel PODroneParceToBO(DroneInParcel droneParcel)
        {
            return new BO.DroneInParcel
            {
                Id = (int)droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = POLocationToBO(droneParcel.Location)
            };
        }

        internal static CustomerInParcel BOCustomerDeliveryToPO(BO.CustomerInParcel customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new CustomerInParcel
            {
                Id = customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }

        internal static BO.CustomerInParcel POCustomerDeliveryToBO(CustomerInParcel customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new BO.CustomerInParcel
            {
                Id = (int)customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }

        /*internal static Parcel BOParcelToPO(BO.Parcel parcel)
        {
            PO.DroneInParcel tempDrone = null;
            if (parcel.Drone != null)
            {
                tempDrone = BODroneParceToPO(parcel.Drone);
            }

            return new Parcel
            {
                Id = parcel.Id,
                CustomerSender = BOCustomerDeliveryToPO(parcel.CustomerSender),
                CustomerReceives = BOCustomerDeliveryToPO(parcel.CustomerReceives),
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                DroneParcel = tempDrone,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered
            };
        }


        /*internal static BO.Parcel POCustomerToBO(PO.Parcel parcel)
        {
            return new BO.Parcel
            {
                Id = parcel.Id,
                CustomerSender = POCustomerDeliveryToBO(parcel.CustomerSender),
                CustomerReceives = POCustomerDeliveryToBO(parcel.CustomerReceives),
                Weight = POEnumWeightCategoriesToBO(parcel.Weight),
                Priority = POEnumPrioritiesToBO(parcel.Priority),
                DroneParcel = PODroneParceToBO(parcel.DroneParcel),
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered
            };
        }

        internal static BO.DroneInCharging PODroneInChargingToBO(PO.DroneInCharging droneCharge)
        {
            return new BO.DroneInCharging
            {
                Id = droneCharge.Id,
                Battery = droneCharge.Battery,
                Time = droneCharge.Time
            };
        }
        internal static PO.ParcelForList BOParcelForListToPO(BO.ParcelForList parcel)
        {
            return new PO.ParcelForList()
            {
                Id = parcel.Id,
                SendCustomer = parcel.SendCustomer,
                ReceiveCustomer = parcel.ReceiveCustomer,
                Weight = (PO.Enums.WeightCategories)parcel.Weight,
                Priority = (PO.Enums.Priorities)parcel.Priority,
                Status = (PO.Enums.ParcelStatuses)parcel.Status
            };
        }

        internal static BO.ParcelForList POParcelForListToBO(PO.ParcelForList parcel)
        {
            return new BO.ParcelForList()
            {
                Id = parcel.Id,
                SendCustomer = parcel.SendCustomer,
                ReceiveCustomer = parcel.ReceiveCustomer,
                Weight = (BO.Enums.WeightCategories)parcel.Weight,
                Priority = (BO.Enums.Priorities)parcel.Priority,
                Status = (BO.Enums.ParcelStatuses)parcel.Status
            };
        }





        internal static ObservableCollection<ParcelForList> BOParcelForListToPO(IEnumerable<BO.ParcelForList> parcelForLists)
        {
            ObservableCollection<PO.ParcelForList> p = new ObservableCollection<ParcelForList>();
            foreach (var parcel in parcelForLists)
            {
                p.Add(new ParcelForList()
                {
                    Id = parcel.Id,
                    SendCustomer = parcel.SendCustomer,
                    ReceiveCustomer = parcel.ReceiveCustomer,
                    Weight = (Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    Status = (Enums.ParcelStatuses)parcel.Status,
                });
            }

            //if (customersForList == null)
            //    return ObservableCollection.Empty<CustomerForList>();
            return p;
        }

        internal static IEnumerable<ParcelToCustomer> BOParcelToCustomerToPO(IEnumerable<BO.ParcelToCustomer> parcelsToCustomer)
        {
            if (parcelsToCustomer == null)
                return Enumerable.Empty<ParcelToCustomer>();
            return parcelsToCustomer.Select(parcel => new ParcelToCustomer()
            {
                Id = parcel.Id,
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                Status = BOEnumParcelStatusToPO(parcel.Status),
                Customer = BOCustomerDeliveryToPO(parcel.Customer)
            });
        }
        internal static Customer BOCustomerToPO(BO.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = BOLocationToPO(customer.Location),
                FromCustomer = BOParcelToCustomerToPO(customer.FromCustomer),
                ToCustomer = BOParcelToCustomerToPO(customer.ToCustomer)
            };
        }

        internal static ObservableCollection<CustomerForList> BOCustomerForListToPO(IEnumerable<BO.CustomerForList> customersForList)
        {
            ObservableCollection<PO.CustomerForList> c = new ObservableCollection<CustomerForList>();
            foreach (var customer in customersForList)
            {
                c.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = customer.NumParcelSentDelivered,
                    NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                    NumParcelReceived = customer.NumParcelReceived,
                    NumParcelWayToCustomer = customer.NumParcelWayToCustomer
                });
            }
            //if (customersForList == null)
            //    return ObservableCollection.Empty<CustomerForList>();
            return c;
        }

        internal static IEnumerable<Parcel> BOParcelToPO(IEnumerable<BO.Parcel> parcels)
        {

            List<PO.Parcel> p = new List<Parcel>();
            foreach (var parcel in parcels)
            {
                p.Add(new PO.Parcel()
                {
                    Id = parcel.Id,
                    CustomerSender = BOCustomerDeliveryToPO(parcel.CustomerSender),
                    CustomerReceives = BOCustomerDeliveryToPO(parcel.CustomerReceives),
                    Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                    Priority = BOEnumPrioritiesToPO(parcel.Priority),
                    DroneParcel = BODroneParceToPO(parcel.DroneParcel),
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    PickedUp = parcel.PickedUp,
                    Delivered = parcel.Delivered
                });
            }
            return p;
        }

        internal static BO.CustomerForList POCustomerForListToBO(CustomerForList customer)
        {
            return new BO.CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                NumParcelSentDelivered = customer.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                NumParcelReceived = customer.NumParcelReceived,
                NumParcelWayToCustomer = customer.NumParcelWayToCustomer
            };
        }

        internal static ParcelByTransfer BOParcelByTransferToPO(BO.ParcelByTransfer parcel)
        {
            return new ParcelByTransfer
            {
                Id = parcel.Id,
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Sender = BOCustomerDeliveryToPO(parcel.Sender),
                Target = BOCustomerDeliveryToPO(parcel.Target),
                SenderLocation = BOLocationToPO(parcel.SenderLocation),
                TargetLocation = BOLocationToPO(parcel.TargetLocation),
                IsDestinationParcel = parcel.IsDestinationParcel,
                Distance = parcel.Distance
            };
        }
        internal static Drone BODroneToPO(BO.Drone drone)
        {
            var tempDelivery = new ParcelByTransfer();
            if (drone.Delivery != null)
            {
                tempDelivery = BOParcelByTransferToPO(drone.Delivery);
            }

            return new Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = BOEnumWeightCategoriesToPO(drone.MaxWeight),
                Status = BOEnumDroneStatusesToPO(drone.Status),
                Battery = drone.Battery,
                Location = BOLocationToPO(drone.Location),
                Delivery = tempDelivery
            };
        }

        internal static ObservableCollection<DroneForList> BODroneForListToPO(IEnumerable<BO.DroneForList> droneForList)
        {
            ObservableCollection<PO.DroneForList> drones = new ObservableCollection<DroneForList>();

            foreach (var drone in droneForList)
                drones.Add(BODorneForListToPO(drone));

            return drones;
        }

        internal static DroneForList BODorneForListToPO(BO.DroneForList drone)
        {
            return new DroneForList()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = BOEnumWeightCategoriesToPO(drone.MaxWeight),
                Battery = Math.Round(drone.Battery),
                Status = BOEnumDroneStatusesToPO(drone.Status),
                Location = BOLocationToPO(drone.Location),
                ParcelDeliveredId = drone.ParcelDeliveredId
            };
        }

        internal static BO.DroneForList PODroneForListToBO(DroneForList drone)
        {
            return new BO.DroneForList()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = POEnumWeightCategoriesToBO(drone.MaxWeight),
                Battery = drone.Battery,
                Status = POEnumDroneStatusesToBO(drone.Status),
                Location = POLocationToBO(drone.Location),
                ParcelDeliveredId = drone.ParcelDeliveredId
            };
        }

        internal static BaseStation BOBaseStationToPO(BO.BaseStation baseStation)
        {
            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                AvailableChargingPorts = baseStation.AvailableChargingPorts,
                Location = BOLocationToPO(baseStation.Location),
                DronesInCharging = BODroneInChargingTOPO(baseStation.DronesInCharging)
            };
        }

        internal static ObservableCollection<PO.DroneInCharging> BODroneInChargingTOPO(IEnumerable<BO.DroneInCharging> dronesInCharging)
        {
            ObservableCollection<PO.DroneInCharging> dronesCharging = new ObservableCollection<DroneInCharging>();
            foreach (var droneCharging in dronesInCharging)
            {
                dronesCharging.Add(new DroneInCharging()
                {
                    Id = droneCharging.Id,
                    Battery = droneCharging.Battery,
                    Time = droneCharging.Time
                });
            }
            return dronesCharging;
        }

        internal static ObservableCollection<PO.BaseStationForList> BOBaseStationForListToPO(IEnumerable<BO.BaseStationForList> baseStationsForList)
        {
            ObservableCollection<PO.BaseStationForList> baseStations = new ObservableCollection<BaseStationForList>();
            foreach (var station in baseStationsForList)
            {
                baseStations.Add(new BaseStationForList()
                {
                    Id = station.Id,
                    Name = station.Name,
                    AvailableChargingPorts = station.AvailableChargingPorts,
                    UsedChargingPorts = station.UsedChargingPorts
                });
            }
            return baseStations;
        }

        internal static BO.BaseStationForList POStationToBO(BaseStationForList station)
        {
            return new BO.BaseStationForList()
            {
                Id = station.Id,
                Name = station.Name,
                AvailableChargingPorts = station.AvailableChargingPorts,
                UsedChargingPorts = station.UsedChargingPorts
            };
        }*/
    }
}