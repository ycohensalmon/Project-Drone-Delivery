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
        Station station;

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
                        catch (Exception)
                        {
                            lock (myBL)
                            {
                                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);
                                if (droneInList.Battery == 100)
                                    break;

                                if (droneInList.Status == DroneStatuses.Available)
                                {
                                    //save the location
                                    tempLocationLat = drone.Location.Latitude;
                                    tempLocationLon = drone.Location.Longitude;

                                    int stationId = myBL.SendDroneToCharge(id);
                                    station = myBL.GetStationById(stationId);

                                    droneInList.Location.Latitude = tempLocationLat;
                                    droneInList.Location.Longitude = tempLocationLon;

                                    //distance fron drone location to base charge
                                    distance = Distance.GetDistanceFromLatLonInKm(tempLocationLat,
                                        tempLocationLon, station.Location.Latitude, station.Location.Longitude);

                                    lossBattery = myBL.GetBatteryIossAvailable() * distance;
                                    droneInList.Battery += lossBattery;
                                }

                                check += (timer / 1000);
                                if (check < (distance / speed))
                                {
                                    if (tempLocationLat > station.Location.Latitude)
                                    {
                                        sumLat = tempLocationLat - station.Location.Latitude;
                                        droneInList.Location.Latitude -= sumLat / (distance / speed);
                                        tempLocationLat -= sumLat / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLat = station.Location.Latitude - tempLocationLat;
                                        droneInList.Location.Latitude += sumLat / (distance / speed);
                                        tempLocationLat += sumLat / (distance / speed);
                                    }

                                    if (tempLocationLon > station.Location.Longitude)
                                    {
                                        sumLon = tempLocationLon - station.Location.Longitude;
                                        droneInList.Location.Longitude -= sumLon / (distance / speed);
                                        tempLocationLon -= sumLon / (distance / speed);
                                    }
                                    else
                                    {
                                        sumLon = station.Location.Longitude - tempLocationLon;
                                        droneInList.Location.Longitude += sumLon / (distance / speed);
                                        tempLocationLon += sumLon / (distance / speed);
                                    }
                                    droneInList.Battery -= lossBattery / (distance / speed);
                                    break;
                                }
                                check = -(timer / 1000);

                                drone = myBL.GetDroneById(id);
                            }
                        }
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
                                drone = myBL.GetDroneById(id);
                            }
                            else
                            {
                                distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude, drone.Location.Longitude,
                                    drone.ParcelInTravel.source.Latitude, drone.ParcelInTravel.source.Longitude);
                                lossBattery = myBL.GetBatteryIossAvailable() * distance;
                                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);

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
                                drone = myBL.GetDroneById(id);
                            }
                            //drone = myBL.GetDroneById(id);
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
