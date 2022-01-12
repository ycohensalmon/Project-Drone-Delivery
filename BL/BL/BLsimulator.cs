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

        TimeSpan TimeSpan;
        Drone drone;
        int id;
        Action updateDelegate;
        Func<bool> stopDelegate;
        BL myBL;

        public Simulator(int id, Action updateDelegate, Func<bool> stopDelegate, BL myBL)
        {

            this.id = id;
            this.updateDelegate = updateDelegate;
            this.stopDelegate = stopDelegate;
            this.myBL = myBL;

            lock (myBL)
            {
                drone = myBL.GetDroneById(id);
            }

            new Thread(LoopThread).Start();
        }

        private void LoopThread()
        {
            double distance, lossBattery;

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
                                int stationId = myBL.SendDroneToCharge(id);
                                station = myBL.GetStationById(stationId);
                                //distance fron drone location to base charge
                                distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude,
                                    drone.Location.Longitude, station.Location.Latitude, station.Location.Longitude);

                                lossBattery = myBL.GetBatteryIossAvailable() * distance;
                                DoTravel(lossBattery, distance, DateTime.Now, id, myBL, updateDelegate);
                                drone = myBL.GetDroneById(id);
                            }
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
                        lock (myBL)
                        {
                            if (drone.ParcelInTravel.InTravel)
                                myBL.DeliveredParcel(id);
                            else
                                myBL.CollectParcelsByDrone(id);
                            Location temp = myBL.GetDroneById(id).Location;
                            distance = Distance.GetDistanceFromLatLonInKm(drone.Location.Latitude, drone.Location.Longitude,
                                temp.Latitude, temp.Longitude);
                            drone = myBL.GetDroneById(id);
                        }

                        //From the beginning of the trip until reaching the destination
                        Thread.Sleep((int)(distance / speed));
                        break;
                    default:
                        break;
                }
                updateDelegate();
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
        private void DoTravel(double lossBattery, double distance, DateTime dateTime, int id, BL myBL, Action updateDelegate)
        {
            lock (myBL)
            {
                DroneInList droneInList = myBL.drones.FirstOrDefault(x => x.Id == id);
                Location temp = droneInList.Location;
                droneInList.Battery += lossBattery;
                droneInList.Location = drone.Location;

                while ((DateTime.Now - dateTime).TotalSeconds < (distance / speed))
                {
                    droneInList.Battery -= lossBattery / (distance / speed);
                    Thread.Sleep(timer);
                    updateDelegate();
                }

                droneInList.Location = temp;
                updateDelegate();
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
