﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {

            public Station GetStationById(int stationId)
            {
                IDAL.DO.Station station = dalObj.GetStationById(stationId);

                Location temp = new Location { Latitude = station.Latitude, Longitude = station.Longitude };

                List<IDAL.DO.DroneCharge> droneChargeIDAL = dalObj.GetDroneCharges().ToList();
                List<DroneCharge> droneChargesBL = new();

                //fill the List droneChargesBL
                foreach (var item in droneChargeIDAL)
                {
                    if (item.StationId == station.Id)
                    {
                        droneChargesBL.Add(new DroneCharge
                        {
                            DroneId = item.DroneId,
                            StationId = item.StationId
                        });
                    }
                }

                return new Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = temp,
                    ChargeSolts = station.ChargeSolts,
                    DroneCharges = droneChargesBL
                };
            }

            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                if (drone == null)
                    throw new ItemNotFoundException(droneId, "Drone");
                return drone;
            }

            public Customer GetCustomerById(int customerId)
            {
                //find the Customer and his parcels fron DL
                IDAL.DO.Customer customer = dalObj.GetCustomerById(customerId);
                List<IDAL.DO.Parcel> parcels = dalObj.GetParcels().ToList();

                Location temp = new Location { Latitude = customer.Latitude, Longitude = customer.Longitude };

                //build to the ParcelsFromCustomer list
                List<ParcelAtCustomer> FromCustomer = new();
                List<ParcelAtCustomer> ToCustomer = new();
                CustomerInParcel customerInParcel = new();
                foreach (var item in parcels)
                {
                    //if this parsel was sent by this customer
                    if (item.SenderId == customer.Id)
                    {
                        //build the CustomerInParcel method
                        customerInParcel.Id = item.TargetId;
                        IDAL.DO.Customer targetCustomer = dalObj.GetCustomerById(item.TargetId);
                        customerInParcel.Name = targetCustomer.Name;

                        //add to the ParcelAtCustomer list
                        FromCustomer.Add(new ParcelAtCustomer
                        {
                            Id = item.Id,
                            Requested = item.Requested,
                            Scheduled = item.Scheduled,
                            PickedUp = item.PickedUp,
                            Delivered = item.Delivered,
                            Weight = (WeightCategory)item.Weight,
                            Priorities = (Priority)item.Priorities,
                            CustomerInParcel = customerInParcel
                        });
                    }

                    //build to the ParcelsToCustomer list

                    //if this parsel was sent to this customer
                    if (item.TargetId == customer.Id)
                    {
                        //build the CustomerInParcel method
                        customerInParcel.Id = item.SenderId;
                        IDAL.DO.Customer senderCustomer = dalObj.GetCustomerById(item.SenderId);
                        customerInParcel.Name = senderCustomer.Name;

                        //add to the ParcelAtCustomer list
                        ToCustomer.Add(new ParcelAtCustomer
                        {
                            Id = item.Id,
                            Requested = item.Requested,
                            Scheduled = item.Scheduled,
                            PickedUp = item.PickedUp,
                            Delivered = item.Delivered,
                            Weight = (WeightCategory)item.Weight,
                            Priorities = (Priority)item.Priorities,
                            CustomerInParcel = customerInParcel
                        });
                    }
                }

                return new Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Location = temp,
                    ParcelsFromCustomer = FromCustomer,
                    ParcelsToCustomer = ToCustomer
                };
            }

            public Parcel GetParcelById(int parcelid)
            {
                IDAL.DO.Parcel parcel = dalObj.GetParcelById(parcelid);
                CustomerInParcel senderOfParcel = new();
                CustomerInParcel targelOfParcel = new();

                senderOfParcel.Id = dalObj.GetCustomerById(parcel.SenderId).Id; ;
                senderOfParcel.Name = dalObj.GetCustomerById(parcel.SenderId).Name;

                targelOfParcel.Id = dalObj.GetCustomerById(parcel.TargetId).Id;
                targelOfParcel.Name = dalObj.GetCustomerById(parcel.TargetId).Name;

                DroneInParcel droneInParcel = new();
                if (parcel.DroneId != 0)
                {
                    DroneInList droneInList = GetDroneById(parcel.DroneId);
                    droneInParcel.Id = droneInList.Id;
                    droneInParcel.Battery = droneInList.Battery;
                    droneInParcel.Location = droneInList.Location;
                }

                return new Parcel
                {
                    Id = parcel.Id,
                    Sender = senderOfParcel,
                    Target =targelOfParcel,
                    Drone = droneInParcel,
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    PickedUp = parcel.PickedUp,
                    Delivered = parcel.Delivered,
                    Weight = (WeightCategory)parcel.Weight,
                    Priorities = (Priority)parcel.Priorities
                };
            }

            public void PrintStations()
            {
                foreach (var item in dalObj.GetStations())
                {
                    Console.WriteLine(GetStationById(item.Id));
                }
            }

            public void PrintCustomers()
            {
                foreach (var item in dalObj.GetCustomers())
                {
                    Console.WriteLine(GetCustomerById(item.Id));
                }
            }

            public void PrintParcels()
            {
                foreach (var item in dalObj.GetParcels())
                {
                    Console.WriteLine(GetParcelById(item.Id));
                }
            }

            public void PrintDrones()
            {
                foreach (var item in dalObj.GetDrones())
                {
                    Console.WriteLine(GetDroneById(item.Id));
                }
            }

            public void PrintParcelsWithoutDrone()
            {
                foreach (var item in dalObj.GetParcels())
                {
                    if (item.DroneId == 0)
                    {
                        Console.WriteLine(GetParcelById(item.Id));
                    }
                }
            }

            public void PrintStationWithChargeSolts()
            {
                foreach (var item in dalObj.GetStations())
                {
                    if (item.ChargeSolts > 0)
                    {
                        Console.WriteLine(GetStationById(item.Id));
                    }
                }
            }
        }
    }
}
