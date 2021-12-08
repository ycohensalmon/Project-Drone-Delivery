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
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
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

            /// <summary>
            /// the function receives drone and connect it to the most importent parcel
            /// </summary>
            /// <param name="droneId">the drone to connect</param>
            public void ConnectDroneToParcel(int droneId)
            {
                //getting the drone and check if it exist and available
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
                if (drone == null)
                    throw new StatusDroneException("the drone does`nt exist");
                if (drone.Status != DroneStatuses.Available)
                    throw new StatusDroneException("connect drone to parcel", drone.Status, DroneStatuses.Available);

                //getting the list of parcels that are not Scheduled 
                List<IDAL.DO.Parcel> parcels = dalObj.GetParcels(x => x.Scheduled == null).ToList();
                if (!parcels.Any())
                    throw new NoParcelException("requested", "scheduled");

                //remove all the parcel that the drone can`t carry
                parcels.RemoveAll(parcels => (int)parcels.Weight > (int)drone.MaxWeight);
                if (parcels.Count == 0)
                    throw new ParcelTooHeavyException(drone.MaxWeight);

                //remove all the parcel that there is no enough battrey in the drone to take them
                parcels.RemoveAll(parcels => CheckEnoughBattery(parcels, drone) < 0);
                if (parcels.Count == 0)
                    throw new NotEnoughBatteryException("make a delivery", drone.Battery);

                //ordering the list to get the most importent parcel in the first plase on the list
                parcels = parcels.OrderBy(parcels => Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(parcels.SenderId).Latitude,
                    dalObj.GetCustomerById(parcels.SenderId).Longitude, drone.Location.Latitude, drone.Location.Longitude)).ToList();
                parcels = parcels.OrderByDescending(t => (int)t.Weight).ToList();
                parcels = parcels.OrderByDescending(t => (int)t.Priorities).ToList();

                //getting the must importent parcel
                IDAL.DO.Parcel myParcel = parcels.First();

                //update the drone in BL
                drone.Status = DroneStatuses.Delivery;
                drone.NumParcel = myParcel.Id;

                //update the drone in DL
                try
                {
                    dalObj.ConnectDroneToParcel(droneId, myParcel.Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }


            /// <summary>
            /// the functaion checks if thre is enough battrey to take this parcel
            /// </summary>
            /// <param name="parcel">the parcel</param>
            /// <param name="drone">the drone</param>
            /// <returns>if there is enough battrey to take this parcel the function will return number>0 if there is no return num<0</returns>
            private double CheckEnoughBattery(IDAL.DO.Parcel parcel, DroneInList drone)
            {
                double batteryIossAvailable, batteryIossWithParcel, allBatteryLoss;

                //loss from his location to sender
                batteryIossAvailable = BatteryIossAvailable(drone.Location.Latitude, drone.Location.Longitude,
                    dalObj.GetCustomerById(parcel.SenderId).Latitude, dalObj.GetCustomerById(parcel.SenderId).Longitude);

                //base station closest to target
                Location temp = GetLocationWithMinDistance(dalObj.GetStations(), dalObj.GetCustomerById(parcel.TargetId));

                //loss from target to base station
                batteryIossAvailable += BatteryIossAvailable(dalObj.GetCustomerById(parcel.TargetId).Latitude,
                dalObj.GetCustomerById(parcel.TargetId).Longitude, temp.Latitude, temp.Longitude);

                //KM from sender lo target
                batteryIossWithParcel = BatteryIossWithParcel(dalObj.GetCustomerById(parcel.SenderId).Latitude,
                dalObj.GetCustomerById(parcel.SenderId).Longitude, dalObj.GetCustomerById(parcel.TargetId).Latitude,
                dalObj.GetCustomerById(parcel.TargetId).Longitude, (int)parcel.Weight);

                allBatteryLoss = batteryIossAvailable + batteryIossWithParcel;
                return (drone.Battery - allBatteryLoss);
            }

            public void CollectParcelsByDrone(int droneId)
            {
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
                if (drone.Status != DroneStatuses.Delivery)
                    throw new StatusDroneException("collect parcel by drone", drone.Status, DroneStatuses.Delivery);

                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.Scheduled == null || myParcel.PickedUp != null)
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
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
                if (drone.Status != DroneStatuses.Delivery)
                    throw new StatusDroneException("delivered parcel to costumer", drone.Status, DroneStatuses.Delivery);

                IDAL.DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.PickedUp == null || myParcel.Delivered != null)
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
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
                if (drone.Status != DroneStatuses.Available)
                    throw new StatusDroneException("send drone to charge", drone.Status, DroneStatuses.Available);

                List<IDAL.DO.Station> station = dalObj.GetStations(x => x.ChargeSolts != 0).ToList();
                if (!station.Any())
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
                DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
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
