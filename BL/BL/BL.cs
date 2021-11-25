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
                    throw new ItemNotFoundException(stationId, "Station");

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
                if (drone.Status != DroneStatuses.Available)
                    throw new StatusDroneException("connect drone to parcel",drone.Status, DroneStatuses.Available);

                List<IDAL.DO.Parcel> parcels = dalObj.GetParcels().ToList();
                parcels = parcels.Where(t => t.Scheduled == DateTime.MinValue).ToList();
                if (parcels.Count == 0)
                    throw new NoParcelException("requested", "scheduled");

                parcels.RemoveAll(parcels => (int)parcels.Weight > (int)drone.MaxWeight);
                if (parcels.Count == 0)
                    throw new ParcelTooHeavyException(drone.MaxWeight);

                double batteryIossAvailable, batteryIossWithParcel, allBatteryLoss;
                foreach (IDAL.DO.Parcel x in parcels)
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
                        parcels.Remove(x);
                }
                if (parcels.Count == 0)
                    throw new NotEnoughBatteryException("make a delivery",drone.Battery);

                parcels.OrderBy(parcels => Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(parcels.SenderId).Latitude,
                    dalObj.GetCustomerById(parcels.SenderId).Longitude, drone.Location.Latitude, drone.Location.Longitude));
                parcels.OrderByDescending(t => (int)t.Weight);
                parcels.OrderByDescending(t => (int)t.Priorities);

                IDAL.DO.Parcel myParcel = parcels.First();

                drone.Status = DroneStatuses.Delivery;
                drone.NumParcel = myParcel.Id;

                try
                {
                    dalObj.ConnectDroneToParcel(droneId, myParcel.Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void CollectParcelsByDrone(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                if (drone.Status != DroneStatuses.Delivery)
                    throw new StatusDroneException("collect parcel by drone", drone.Status, DroneStatuses.Delivery);

                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.Scheduled == DateTime.MinValue || myParcel.PickedUp != DateTime.MinValue)
                    throw new NoParcelException("scheduled", "picked up");
                if (myParcel.DroneId != droneId)
                    throw new NotConnectException(droneId, myParcel.DroneId, myParcel.Id);

                IDAL.DO.Customer myCustomer = dalObj.GetCustomerById(myParcel.SenderId);

                //loss from his location to sender
                double batteryIossAvailable = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                    myCustomer.Latitude, myCustomer.Longitude);
                drone.Battery -= batteryIossAvailable;

                //update location of the drone
                drone.Location.Latitude = myCustomer.Latitude;
                drone.Location.Longitude = myCustomer.Longitude;

                try
                {
                    //update parsel
                    dalObj.CollectParcelByDrone(myParcel.Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void DeliveredParcel(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                if (drone.Status != DroneStatuses.Delivery)
                    throw new StatusDroneException("delivered parcel to costumer",drone.Status, DroneStatuses.Delivery);

                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.PickedUp == DateTime.MinValue || myParcel.Delivered == DateTime.MinValue)
                    throw new NoParcelException("picked up", "delivered");
                if (myParcel.DroneId != droneId)
                    throw new NotConnectException(droneId, myParcel.DroneId, myParcel.Id);


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

                try
                {
                    dalObj.DeliveredParcel(myParcel.Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }

            }

            public void SendDroneToCharge(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                if (drone.Status != DroneStatuses.Available)
                    throw new StatusDroneException("send drone to charge", drone.Status, DroneStatuses.Available);

                List<IDAL.DO.Station> station = dalObj.GetStations().ToList();
                station.RemoveAll(s => s.ChargeSolts == 0);
                if (station.Count == 0)
                    throw new NoChargeSlotException();

                station.OrderBy(s => Distance.GetDistanceFromLatLonInKm(s.Latitude, s.Longitude, drone.Location.Latitude, drone.Location.Longitude));


                double battryLoss = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                    station.First().Latitude, station.First().Longitude);
                if (drone.Battery - battryLoss < 0)
                    throw new NotEnoughBatteryException("go to the base charge");

                //update drone
                drone.Battery -= battryLoss;
                drone.Location.Latitude = station.First().Latitude;
                drone.Location.Longitude = station.First().Longitude;
                drone.Status = DroneStatuses.Maintenance;

                try
                {
                    //update Base charge and list of drone charge
                    dalObj.SendDroneToBaseCharge(droneId, station.First().Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }

            }

            public void ReleaseDroneFromCharging(int droneId, double timeCharge)
            {
                DroneInList drone = GetDroneById(droneId);
                if (drone.Status != DroneStatuses.Maintenance)
                    throw new StatusDroneException("release drone from charging", drone.Status, DroneStatuses.Maintenance);

                if (drone.Battery + timeCharge * (LoadingRate / 60) < 100) 
                    drone.Battery += timeCharge * (LoadingRate / 60);
                else
                    drone.Battery = 100;
                drone.Status = DroneStatuses.Available;

                try
                {
                    dalObj.ReleaseDroneFromCharging(droneId);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void UpdateDrone(int droneId, string model)
            {
                DroneInList drone = GetDroneById(droneId);
                drone.Model = model;

                dalObj.UpdateDrone(droneId, model);
            }

            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                if (drone.Id != droneId)
                    throw new ItemNotFoundException(droneId, "DroneInList");
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
