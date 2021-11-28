using System;
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
            public void UpdateDrone(int droneId, string model)
            {
                DroneInList drone = GetDroneById(droneId);
                drone.Model = model;

                try
                {
                    dalObj.UpdateDrone(droneId, model);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void UpdateBase(int stationId, string newName, string newChargeSolts)
            {
                int result = 0;
                if (newChargeSolts != "")
                {
                    IDAL.DO.Station station = dalObj.GetStationById(stationId);
                    result = Int32.Parse(newChargeSolts);
                    // אם ההמרה לא עבדה ונכנס רק אותיות
                    List<IDAL.DO.DroneCharge> droneCharge = dalObj.GetDroneCharges().ToList();
                    foreach (var item in droneCharge)
                    {
                        if (item.StationId == station.Id)
                            result--;
                    }
                }
                try
                {
                    dalObj.UpdateBase(stationId, newName, newChargeSolts, result);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void UpdateCustomer(int customerID, string newName, string newPhone)
            {
                dalObj.UpdateCustomer(customerID, newName, newPhone);
            }

            public void ConnectDroneToParcel(int droneId)
            {
                DroneInList drone = GetDroneById(droneId);
                if (drone.Status != DroneStatuses.Available)
                    throw new StatusDroneException("connect drone to parcel", drone.Status, DroneStatuses.Available);

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
                    throw new NotEnoughBatteryException("make a delivery", drone.Battery);

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
                    throw new StatusDroneException("delivered parcel to costumer", drone.Status, DroneStatuses.Delivery);

                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.PickedUp == DateTime.MinValue || myParcel.Delivered != DateTime.MinValue)
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
        }
    }

}
