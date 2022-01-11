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
        const double speed = 0.75;

        Drone drone;

        public Simulator(int id, Action updateDelegate, Func<bool> stopDelegate, BL myBL)
        {
            lock (myBL)
            {
                drone = myBL.GetDroneById(id);
            }

            double distance;

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
                            Thread.Sleep(timer);
                        }
                        catch (NotEnoughBatteryException)
                        {
                            Station station;
                            lock (myBL)
                            {
                                /////////// station = 
                                //////drone = myBL.GetDroneById(id);
                                int stationId = myBL.SendDroneToCharge(id);
                                station = myBL.GetStationById(stationId);
                            }
                            distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude,
                                drone.Location.Longitude, station.Location.Latitude, station.Location.Longitude);
                            //time of way from his lication to base charge
                            Thread.Sleep((int)(distance / speed));
                        }
                        catch (NoParcelException)
                        {
                            Thread.Sleep(timer);
                        }
                        catch (ParcelTooHeavyException)
                        {
                            Thread.Sleep(timer);
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
                        Thread.Sleep(timer);
                        break;

                    case DroneStatuses.Delivery:
                        distance = myBL.GetDroneById(id).ParcelInTravel.Distance;
                        //From the beginning of the trip until reaching the destination
                        Thread.Sleep((int)(distance / speed));

                        lock (myBL)
                        {
                            if (drone.ParcelInTravel.InTravel)
                                myBL.DeliveredParcel(id);
                            else
                                myBL.CollectParcelsByDrone(id);
                            /////////drone = myBL.GetDroneById(id);
                        }
                        Thread.Sleep(timer);
                        break;
                    default:
                        break;
                }
                updateDelegate();
            }
        }

        private double GetBatteryPercentages(int id, BL myBL)
        {
            lock (myBL)
            {
                var drone = myBL.dalObj.GetDroneCharges(x => x.DroneId == id).First();
                double presentOfCharge = (DateTime.Now - drone.EnteryTime).Value.TotalSeconds *
                    (myBL.getLoadingRate() / 60);
                drone.EnteryTime = DateTime.Now;
                return presentOfCharge;
            }
        }
    }
}
        
//        case DroneStatuses.Available:
//                        lock (myBL)
//                        {
//            try
//            {
//                myBL.AssignDroneToParcel(drone.Id);
//                drone = myBL.GetDrone(drone.Id);
//            }
//            catch (EmptyListException) { }
//            catch (NoBatteryException) //if there is not enough battery to make a delivery for any of the parcels
//            {
//                try
//                {
//                    myBL.ChargeDrone(drone.Id);
//                    var tmp = myBL.GetDrone(drone.Id).CurrentLocation;
//                    Thread.Sleep((int)(BL.getDistance(tmp, drone.CurrentLocation) / speed)); //update the drone only after the drone has reached the station
//                    drone = myBL.GetDrone(drone.Id);
//                }
//                //in both cases, the drone will wait for the next cycle and try again to see if there is 
//                //an available station for him to reach
//                //(there might be a case where there was a new parcel that the drone is able to carry)
//                catch (EmptyListException) { } //if there is no available charge slots try again in the next cycle
//                catch (NoBatteryException) { } //if there is not enough battery to get to the nearest station with available charge slots
//            }
//        }
//                        break;
//    }
//}
