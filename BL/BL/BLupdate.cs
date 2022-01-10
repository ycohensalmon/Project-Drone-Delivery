using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using BlApi;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL : IBL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model)
        {
            DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
            drone.Model = model;

            try
            {
                lock (dalObj)
                {
                    dalObj.UpdateDrone(droneId, model);
                } 
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBase(int stationId, string newName, string newChargeSolts)
        {
            lock (dalObj)
            {
                int result = 0;
                if (newChargeSolts != "")
                {
                    DO.Station station = dalObj.GetStationById(stationId);
                    result = Int32.Parse(newChargeSolts);
                    // אם ההמרה לא עבדה ונכנס רק אותיות
                    List<DO.DroneCharge> droneCharge = dalObj.GetDroneCharges().ToList();
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int customerID, string newName, string newPhone)
        {
            lock (dalObj)
            {
                dalObj.UpdateCustomer(customerID, newName, newPhone);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
            lock (dalObj)
            {
                //getting the list of parcels that are not Scheduled 
                IEnumerable<DO.Parcel> parcels = dalObj.GetParcels(x => x.Scheduled == null);
                if (!parcels.Any())
                    throw new NoParcelException("requested", "scheduled");

                DO.Parcel worth = parcels.FirstOrDefault(x => CheckEnoughBattery(x, drone) > 0 && (int)x.Weight <= (int)drone.MaxWeight);
                if (worth.Id == 0)
                    throw new ParcelTooHeavyException(drone.MaxWeight);
            
                foreach (var x in parcels)
                {
                    worth = GetWorthParcel((DO.Parcel)worth, x, drone);
                }

                //update the drone in BL
                drone.Status = DroneStatuses.Delivery;
                drone.NumParcel = worth.Id;

                //update the drone in DL
                try
                {
                    dalObj.ConnectDroneToParcel(droneId, worth.Id);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }
        }

        /// <summary>
        /// Compares between 2 parcels 
        /// </summary>
        /// <param name="worth">the parsel that found most compatible at this point</param>
        /// <param name="check">the parcel we want to compare with the "worth" parcel</param>
        /// <param name="drone">the drone we wamt connect to parcel</param>
        /// <returns>returns the more compatible parcel</returns>
        private DO.Parcel GetWorthParcel(DO.Parcel worth, DO.Parcel check, DroneInList drone)
        {
            //if the drone can`t carry this parcel ("check") 
            if((int)check.Weight > (int)drone.MaxWeight || CheckEnoughBattery(check, drone) < 0)
                return worth;

            //the parcel "worth" found more compatible (becuse the "Priorities")
            if ((int)worth.Priorities > (int)check.Priorities)
                return worth;

            //the parcel "check" found more compatible (becuse the "Priorities")
            if ((int)worth.Priorities < (int)check.Priorities)
                return check;

            //the parcel "worth" found more compatible (becuse the "Weight")
            if ((int)worth.Weight > (int)check.Weight)
                return worth;

            //the parcel "check" found more compatible (becuse the "Weight")
            if ((int)worth.Weight < (int)check.Weight)
                return check;

            lock (dalObj)
            {
                //getting the distance from drone to parcel "worth"
                double theDistanceToWorth = Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(worth.SenderId).Latitude,
                    dalObj.GetCustomerById(worth.SenderId).Longitude, drone.Location.Latitude, drone.Location.Longitude);

                //getting the distance from drone to parcel "check"
                double theDistanceToCheck = Distance.GetDistanceFromLatLonInKm(dalObj.GetCustomerById(check.SenderId).Latitude,
                    dalObj.GetCustomerById(check.SenderId).Longitude, drone.Location.Latitude, drone.Location.Longitude);

                //the parcel "worth" is closer
                if (theDistanceToWorth < theDistanceToCheck)
                    return worth;

                //the parcel "check" closer
                if (theDistanceToWorth > theDistanceToCheck)
                    return check;
                return worth;
            }
        }

        /// <summary>
        /// the functaion checks if thre is enough battrey to take this parcel
        /// </summary>
        /// <param name="parcel">the parcel</param>
        /// <param name="drone">the drone</param>
        /// <returns>if there is enough battrey to take this parcel the function will return number>0 if there is no return num<0</returns>
        private double CheckEnoughBattery(DO.Parcel parcel, DroneInList drone)
        {
            lock (dalObj)
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectParcelsByDrone(int droneId)
        {
            DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
            if (drone.Status != DroneStatuses.Delivery)
                throw new StatusDroneException("collect parcel by drone", drone.Status, DroneStatuses.Delivery);
            lock (dalObj)
            {
                DO.Parcel myParcel = dalObj.GetParcelById(drone.NumParcel);
                if (myParcel.Scheduled == null || myParcel.PickedUp != null)
                    throw new NoParcelException("scheduled", "picked up");
                if (myParcel.DroneId != droneId)
                    throw new NotConnectException(droneId, myParcel.DroneId, myParcel.Id);

                DO.Customer myCustomer = dalObj.GetCustomerById(myParcel.SenderId);

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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveredParcel(int droneId)
        {
            DroneInList drone;

            DO.Parcel myParcel = GetParcelWasConnectToParcel(droneId, out drone);
            if (myParcel.PickedUp == null || myParcel.Delivered != null)
                throw new NoParcelException("picked up", "delivered");
            if (myParcel.DroneId != droneId)
                throw new NotConnectException(droneId, myParcel.DroneId, myParcel.Id);

            lock (dalObj)
            {
                DO.Customer myCustomer = dalObj.GetCustomerById(myParcel.TargetId);

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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel GetParcelWasConnectToParcel(int droneId, out DroneInList drone)
        {
            drone = drones.FirstOrDefault(x => x.Id == droneId);
            if (drone.Status != DroneStatuses.Delivery)
                throw new StatusDroneException("delivered parcel to costumer", drone.Status, DroneStatuses.Delivery);
            lock (dalObj)
            {
                return dalObj.GetParcelById(drone.NumParcel);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharge(int droneId)
        {
            //getting the drone
            DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
            if (drone.Status != DroneStatuses.Available)
                throw new StatusDroneException("send drone to charge", drone.Status, DroneStatuses.Available);
            lock (dalObj)
            {
                //getting the stations that available
                List<DO.Station> station = dalObj.GetStations(x => x.ChargeSolts != 0).ToList();
                if (!station.Any())
                    throw new NoChargeSlotException();

                //order the station by closest
                station = station.OrderBy(s => Distance.GetDistanceFromLatLonInKm(s.Latitude, s.Longitude, drone.Location.Latitude, drone.Location.Longitude)).ToList();

                //checking if the drone can go to the closest station
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromCharging(int droneId)
        {
            double timeCharge;
            try
            {
                lock (dalObj)
                {
                    timeCharge = dalObj.ReleaseDroneFromCharging(droneId);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }

            DroneInList drone = drones.FirstOrDefault(x => x.Id == droneId);
            if (drone.Status != DroneStatuses.Maintenance)
                throw new StatusDroneException("release drone from charging", drone.Status, DroneStatuses.Maintenance);

            if (drone.Battery + timeCharge * (LoadingRate / 60) < 100)
                drone.Battery += timeCharge * (LoadingRate / 60);
            else
                drone.Battery = 100;
            drone.Status = DroneStatuses.Available;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void confirmPackage(int userId, int parcelId)
        {
          // dalObj.


        }

        public void ClearDroneCharge() { lock (dalObj) { dalObj.ClearDroneCharge(); } }

        public void ActivSimulator(int id, Action updateDelegate, Func<bool> stopDelegate)
        {
            new Simulator(id, updateDelegate, stopDelegate, this);
        }
    }
}
