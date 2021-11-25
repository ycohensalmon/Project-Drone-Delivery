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
