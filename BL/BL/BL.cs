﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public void NewStation(Station station)
            {
                try
                {
                    dalObj.NewStation(new IDAL.DO.Station
                    {
                        Id = station.Id,
                        Name = station.Name,
                        Latitude = station.Location.Latitude,
                        Longitude = station.Location.Longitude,
                        ChargeSolts = station.ChargeSolts
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void NewDroneInList(DroneInList drone, int stationId)
            {
                if (dalObj.GetStations().First(station => station.Id == stationId).Id != stationId)
                    throw new ItemNotFoundException(stationId, "Drone");

                try
                {
                    drone.Battery = rand.Next(20, 40);
                    drone.Battery += rand.NextDouble();
                    drone.Status = (DroneStatuses)1;
                    drone.NumParcel = 0;
                    drone.Location = new Location
                    {
                        Latitude = dalObj.GetStationById(stationId).Latitude,
                        Longitude = dalObj.GetStationById(stationId).Longitude
                    };

                    drones.Add(drone);

                    dalObj.NewDrone(new IDAL.DO.Drone
                    {
                        Id = drone.Id,
                        Model = drone.Model,
                        MaxWeight = (IDAL.DO.WeightCategory)drone.MaxWeight
                    });
                    dalObj.SendDroneToBaseCharge(drone.Id, stationId);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
                
            }

            public void NewCostumer(Customer customer)
            {
                try
                {
                    dalObj.NewCostumer(new IDAL.DO.Customer
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Latitude = customer.Location.Latitude,
                        Longitude = customer.Location.Longitude
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void NewParcel(Parcel parcel, int senderID, int receiveID)
            {
                try
                {
                    dalObj.NewParcel(new IDAL.DO.Parcel
                    {
                        Id = DalObject.DataSource.SerialNum++,
                        SenderId = senderID,
                        TargetId = receiveID,
                        DroneId = 0,
                        Requested = DateTime.Now,
                        Scheduled = DateTime.MinValue,
                        PickedUp = DateTime.MinValue,
                        Delivered = DateTime.MinValue,
                        Weight = (IDAL.DO.WeightCategory)parcel.Weight,
                        Priorities = (IDAL.DO.Priority)parcel.Priorities
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void ConnectDroneToParcel(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                List<IDAL.DO.Parcel> tempList = dalObj.GetParcels().ToList();
                tempList = tempList.Where(t => t.Scheduled == DateTime.MinValue).ToList();
                //if(tempList.count == 0) there is no parcels, than  "threw"
                tempList.RemoveAll(p => (int)p.Weight > (int)drone.MaxWeight);
                //if(tempList.count == 0) there is no parcels, than  "threw;
                tempList.OrderBy(t => Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(t.SenderId).Latitude,
                    dalObj.GetCustomerById(t.SenderId).Longitude, drone.Location.Latitude, drone.Location.Longitude));
                tempList.OrderByDescending(t => (int)t.Weight);
                tempList.OrderByDescending(t => (int)t.Priorities);

                double batteryIossAvailable, batteryIossWithParcel, allBatteryLoss;
                foreach (IDAL.DO.Parcel x in tempList)
                {
                    //loss from his location to sender
                    batteryIossAvailable = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                        dalObj.GetCustomerById(x.SenderId).Latitude, dalObj.GetCustomerById(x.SenderId).Longitude);

                    //base station closest to target
                    Location temp = GetLocationWithMinDistance(dalObj.GetStations(), dalObj.GetCustomerById(x.TargetId));

                    //loss from target to base station
                    batteryIossAvailable += BatteryIossAvailable(dalObj.GetCustomerById(x.TargetId).Latitude,
                    dalObj.GetCustomerById(x.TargetId).Longitude, temp.Latitude, temp.Longitude);

                    //KM from sender lo target
                    batteryIossWithParcel = BatteryIossWithParcel(dalObj.GetCustomerById(x.SenderId).Latitude,
                    dalObj.GetCustomerById(x.SenderId).Longitude, dalObj.GetCustomerById(x.TargetId).Latitude,
                    dalObj.GetCustomerById(x.TargetId).Longitude, (int)x.Weight);

                    allBatteryLoss = batteryIossAvailable + batteryIossWithParcel;
                    if (drone.Battery - allBatteryLoss < 0)
                        tempList.Remove(x);
                }
                //if(tempList.count == 0) there is no parcels, than  "threw;

                IDAL.DO.Parcel myParcel= tempList.First();

                drone.Status = DroneStatuses.Delivery;
                drone.NumParcel = myParcel.Id;

                dalObj.ConnectDroneToParcel(droneId, myParcel.Id);
            }


            public void CollectParcelsByDrone(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);

                if (myParcel.Scheduled != DateTime.MinValue && myParcel.PickedUp == DateTime.MinValue && myParcel.DroneId == droneId)
                {
                    IDAL.DO.Customer myCustomer = dalObj.GetCustomerById(myParcel.SenderId);

                    //loss from his location to sender
                    double batteryIossAvailable = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                        myCustomer.Latitude, myCustomer.Longitude);
                    drone.Battery -= batteryIossAvailable;

                    //update location of the drone
                    drone.Location.Latitude = myCustomer.Latitude;
                    drone.Location.Longitude = myCustomer.Longitude;

                    //update parsel
                    dalObj.CollectParcelByDrone(myParcel.Id);
                }
                //במקרה שאי אפשר לאסוף את החבילה תיזרק חריגה 
            }

            public void DeliveredParcel(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.PickedUp != DateTime.MinValue && myParcel.Delivered == DateTime.MinValue && myParcel.DroneId == droneId)
                {
                    IDAL.DO.Customer myCustomer = dalObj.GetCustomerById(myParcel.TargetId);

                    //loss from sender lo target
                    double batteryIossWithParcel = BatteryIossWithParcel(drone.Location.Latitude, drone.Location.Longitude,
                        myCustomer.Latitude, myCustomer.Longitude, (int)myParcel.Weight);
                    drone.Battery -= batteryIossWithParcel;

                    //update location of the drone
                    drone.Location.Latitude = myCustomer.Latitude;
                    drone.Location.Longitude = myCustomer.Longitude;

                    drone.Status = DroneStatuses.Available;
                    drone.NumParcel = 0;

                    dalObj.DeliveredParcel(myParcel.Id);
                }
            }

            public void SendDroneToCharge(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                //רק רחפן פנוי יוכל להישלח לטעינה
                List<IDAL.DO.Station> station = dalObj.GetStations().ToList();
                station.RemoveAll(s => s.ChargeSolts == 0);
                //if station.count==0 "threw;

                station.OrderBy(s => Distance.GetDistanceFromLatLonInKm(s.Latitude, s.Longitude, drone.Location.Latitude, drone.Location.Longitude));


                double battryLoss = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                    station.First().Latitude, station.First().Longitude);
                //if (drone.Battery - battryLoss < 0)
                //threw.....

                //update drone
                drone.Battery -= battryLoss;
                drone.Location.Latitude = station.First().Latitude;
                drone.Location.Longitude = station.First().Longitude;
                drone.Status = DroneStatuses.Maintenance;

                //update Base charge and list of drone charge
                dalObj.SendDroneToBaseCharge(droneId, station.First().Id);
            }

            public void ReleaseDroneFromCharging(int droneId, double timeCharge)
            {
                DroneInList drone = GetDroneById(droneId);
                //רק רחפן בתחזוקה יוכל להשתחרר מטעינה

                if (drone.Battery + timeCharge * (LoadingRate / 60) < 100) 
                    drone.Battery += timeCharge * (LoadingRate / 60);
                else
                    drone.Battery = 100;
                drone.Status = DroneStatuses.Available;

                dalObj.ReleaseDroneFromCharging(droneId);
            }


            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                return drone;
            }

            double BatteryIossAvailable(double lat1, double lon1, double lat2, double lon2)
            {
                return (Distance.GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2)) * Available;
            }

            double BatteryIossWithParcel(double lat1, double lon1, double lat2, double lon2, int Weight)
            {
                double batteryIoss = Distance.GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2);
                switch (Weight)
                {
                    case 0:
                        batteryIoss *= LightParcel;
                        break;
                    case 1:
                        batteryIoss *= MediumParcel;
                        break;
                    case 2:
                        batteryIoss *= HeavyParcel;
                        break;
                }
                return batteryIoss;
            }
        }
    }
}