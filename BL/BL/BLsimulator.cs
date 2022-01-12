using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using System.Threading;
using static BL.BL;


namespace BL
{
    class Simulator
    {
        const int timer = 1000;
        const double speed = 0.5;

        //TimeSpan TimeSpan;
        Drone drone;
        double distance, lossBattery, tempLocationLat, tempLocationLon, check = 0, sumLat, sumLon;
        

        public Simulator(int id, Action updateDelegate, Func<bool> stopDelegate, BL myBL)
        {
            lock (myBL)
            {
                drone = myBL.GetDroneById(id);
            }
            check -= (timer/1000);

            while (!stopDelegate())
            {
                switch (drone.Status)
                {
                    case DroneStatuses.Available:
                        try
                        {
                            lock (myBL)
                            {
                                myBL.ConnectDroneToParcel(id);
                                drone = myBL.GetDroneById(id);
                            }
                        }
                        catch (NotEnoughBatteryException)
                        {
                            Station station;
                            
                            lock (myBL)
                            {
                                //save the location
                                tempLocationLat = drone.Location.Latitude;
                                tempLocationLon = drone.Location.Longitude;

                                int stationId = myBL.SendDroneToCharge(id);
                                station = myBL.GetStationById(stationId);
                                //distance fron drone location to base charge
                                distance = Distance.GetDistanceFromLatLonInKm(tempLocationLat,
                                    tempLocationLon, station.Location.Latitude, station.Location.Longitude);

                                lossBattery = myBL.GetBatteryIossAvailable() * distance;
                                //func to show the changing of battery when drone travel to base station
                                DoTravel(DateTime.Now, id, myBL, updateDelegate);
                                drone = myBL.GetDroneById(id);
                            }
                        }
                        catch (NoParcelException) { }  //the drone will wait for new parcel
                        catch (ParcelTooHeavyException) { } //the drone will wait for new parcel
                        break;

                    case DroneStatuses.Maintenance:
                        lock (myBL)
                        {
                            if (drone.Battery + GetBatteryPercentages(id, myBL) >= 100)
                                myBL.ReleaseDroneFromCharging(id);
                            else
                                myBL.drones.FirstOrDefault(x => x.Id == id).Battery += GetBatteryPercentages(id, myBL);
                            drone = myBL.GetDroneById(id);
                        }
                        break;

                    case DroneStatuses.Delivery:
                        lock (myBL)
                        {
                            if (drone.ParcelInTravel.InTravel)
                            {
                                distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude, drone.Location.Longitude,
                                   drone.ParcelInTravel.Destination.Latitude, drone.ParcelInTravel.Destination.Longitude);
                                lossBattery = GetBatteryIossWithParcel(myBL) * distance;
                                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);
                                check += (timer / 1000);
                                if (check < (distance / speed))
                                {
                                    if (drone.Location.Latitude > drone.ParcelInTravel.Destination.Latitude)
                                    {
                                        sumLat = drone.Location.Latitude - drone.ParcelInTravel.Destination.Latitude;
                                        droneInList.Location.Latitude -= sumLat / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLat = drone.ParcelInTravel.Destination.Latitude - drone.Location.Latitude;
                                        droneInList.Location.Latitude += sumLat / (distance / speed);
                                    }

                                    if (drone.Location.Longitude > drone.ParcelInTravel.Destination.Longitude)
                                    {
                                        sumLon = drone.Location.Longitude - drone.ParcelInTravel.Destination.Longitude;
                                        droneInList.Location.Longitude -= sumLon / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLon = drone.ParcelInTravel.Destination.Longitude - drone.Location.Longitude;
                                        droneInList.Location.Longitude += sumLon / (distance / speed);
                                    }
                                    droneInList.Battery -= lossBattery / (distance / speed);
                                    break;
                                }

                                check = -(timer / 1000);
                                myBL.DeliveredParcel(id);
                                droneInList.Battery += lossBattery;
                            }
                            else
                            {
                                distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude, drone.Location.Longitude,
                                    drone.ParcelInTravel.source.Latitude, drone.ParcelInTravel.source.Longitude);
                                lossBattery = myBL.GetBatteryIossAvailable() * distance;
                                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);
                                //calculate location

                                check += (timer / 1000);
                                if (check < (distance / speed))
                                {
                                    if (drone.Location.Latitude > drone.ParcelInTravel.source.Latitude)
                                    {
                                        sumLat = drone.Location.Latitude - drone.ParcelInTravel.source.Latitude;
                                        droneInList.Location.Latitude -= sumLat / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLat = drone.ParcelInTravel.source.Latitude - drone.Location.Latitude;
                                        droneInList.Location.Latitude += sumLat / (distance / speed);
                                    }

                                    if (drone.Location.Longitude > drone.ParcelInTravel.source.Longitude)
                                    {
                                        sumLon = drone.Location.Longitude - drone.ParcelInTravel.source.Longitude;
                                        droneInList.Location.Longitude -= sumLon / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLon = drone.ParcelInTravel.source.Longitude - drone.Location.Longitude;
                                        droneInList.Location.Longitude += sumLon / (distance / speed);
                                    }

                                    droneInList.Battery -= lossBattery / (distance / speed);
                                    break;
                                }

                                check = -(timer / 1000);
                                myBL.CollectParcelsByDrone(id);
                                droneInList.Battery += lossBattery;
                            }
                            drone = myBL.GetDroneById(id);
                        }
                        break;
                    default:
                        break;
                }
                updateDelegate();
                Thread.Sleep(timer);
            }
        }

        private double GetBatteryPercentages(int id, BL myBL)
        {
            lock (myBL) lock (myBL.dalObj) 
                { 
                    var drone = myBL.dalObj.GetDroneCharges(x => x.DroneId == id).First();
                    double presentOfCharge = (DateTime.Now - drone.EnteryTime).Value.TotalSeconds *
                        (myBL.getLoadingRate() / 60);
                    drone.EnteryTime = DateTime.Now;
                    return presentOfCharge;
                }
        }

        /// <summary>
        /// the function set the battery of the drone when it travel end finished when the drone reaches his destination
        /// </summary>
        /// <param name="lossBattery">loss battery of all travel</param>
        /// <param name="distance">the distance from sourse and destination</param>
        /// <param name="dateTime">the time was the function called</param>
        /// <param name="id">the id of the drone</param>
        /// <param name="myBL">object of BL</param>
        /// <param name="updateDelegate">action</param>
        private void DoTravel(DateTime dateTime, int id, BL myBL, Action updateDelegate)
        {
            lock (myBL)
            {
                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);
                var targetLocationLat = droneInList.Location.Latitude;
                var targetLocationLon = droneInList.Location.Longitude;
                droneInList.Location.Latitude = tempLocationLat;
                droneInList.Location.Longitude = tempLocationLon;
                droneInList.Battery += lossBattery;
                updateDelegate();

                while ((DateTime.Now - dateTime).TotalSeconds < (distance / speed))
                {
                    droneInList.Battery -= lossBattery / (distance / speed);
                    Thread.Sleep(timer);
                    updateDelegate();
                }

                droneInList.Location.Latitude = targetLocationLat;
                droneInList.Location.Longitude = targetLocationLon;
                updateDelegate();
            }
        }

        private double GetBatteryIossWithParcel(BL myBL)
        {
            switch (drone.ParcelInTravel.Weight)
            {
                case WeightCategory.Light:
                    return myBL.GetBatteryIossLightParcel();
                case WeightCategory.Medium:
                    return myBL.GetBatteryIossMediumParcel();
                case WeightCategory.Heavy:
                    return myBL.GetBatteryIossHeavyParcel();
                default:
                    return myBL.GetBatteryIossAvailable();
            }
        }
    }
}
