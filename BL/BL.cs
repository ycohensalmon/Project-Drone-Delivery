using System;
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
            public void NewStation(Station x)
            {
                try
                {
                    dalObj.NewStation(new IDAL.DO.Station
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude,
                        ChargeSolts = x.ChargeSolts
                    });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            public void NewCostumer(Customer x)
            {
                try
                {
                    dalObj.NewCostumer(new IDAL.DO.Customer
                    {
                        Id = x.Id,
                        Name =  x.Name,
                        Phone = x.Phone,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            public void NewDroneInList(DroneInList x, int numStation)
            {
                x.Battery = rand.Next(20,40);
                x.Battery += rand.NextDouble();
                x.Status = (DroneStatuses)1;
                x.NumParcel = 0;
                x.Location = new Location
                {
                    Latitude = dalObj.GetStationById(numStation).Latitude,
                    Longitude = dalObj.GetStationById(numStation).Longitude
                };

                drones.Add(x);

                dalObj.NewDrone(new IDAL.DO.Drone
                {
                    Id = x.Id,
                    Model = x.Model,
                    MaxWeight = (IDAL.DO.WeightCategory)x.MaxWeight
                });                                                                        
                dalObj.SendDroneToBaseCharge(x.Id, numStation);
            }

            public void NewParcel(Parcel x, int senderID, int receiveID)
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
                    Weight = (IDAL.DO.WeightCategory)x.Weight,
                    Priorities = (IDAL.DO.Priority)x.Priorities
                });
            }

            public void connectDroneToParcel(int droneId)
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
                    //KM from his location to sender
                    batteryIossAvailable = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude, drone.Location.Longitude,
                        dalObj.GetCustomerById(x.SenderId).Latitude, dalObj.GetCustomerById(x.SenderId).Longitude);

                    //base station closest to target
                    Location temp = GetLocationWithMinDistance(dalObj.GetStations(), dalObj.GetCustomerById(x.TargetId));

                    //KM from target to base station
                    batteryIossAvailable += Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(x.TargetId).Latitude,
                    dalObj.GetCustomerById(x.TargetId).Longitude, temp.Latitude, temp.Longitude);
                    batteryIossAvailable *= Available;  //KM * %loss in state "Available"

                    //KM from sender lo target
                    batteryIossWithParcel = Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(x.SenderId).Latitude,
                    dalObj.GetCustomerById(x.SenderId).Longitude, dalObj.GetCustomerById(x.TargetId).Latitude,
                    dalObj.GetCustomerById(x.TargetId).Longitude);
                    
                    int Weight = (int)x.Weight;
                    switch (Weight)
                    {
                        case 0:
                            batteryIossWithParcel *= LightParcel;
                            break;
                        case 1:
                            batteryIossWithParcel *= MediumParcel;
                            break;
                        case 2:
                            batteryIossWithParcel *= HeavyParcel;
                            break;
                    }

                    allBatteryLoss = batteryIossAvailable + batteryIossWithParcel;
                    if (drone.Battery - allBatteryLoss < 0)
                        tempList.Remove(x);
                }
                //if(tempList.count == 0) there is no parcels, than  "threw;

                drone.Status = DroneStatuses.Delivery;

                IDAL.DO.Parcel myParcel= tempList.First();
                dalObj.ConnectDroneToParcel(droneId, myParcel.Id);
            }

            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                return drone;
            }

            public void sendDroneToCharge(int droneID, int baseStatiunID)
            {

            }
        }
    }
}
