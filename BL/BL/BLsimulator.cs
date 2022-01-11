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
                            }
                            Thread.Sleep(timer);
                        }
                        catch (NotEnoughBatteryException)
                        {
                            Station station;
                            lock (myBL)
                            {
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
                            //wait //check
                        }
                        catch (ParcelTooHeavyException)
                        {
                            //stop
                        }
                        break;
                    case DroneStatuses.Maintenance:
                        lock (myBL)
                        {
                            drone.Battery += GetBatteryPercentages(id, myBL);
                            if (drone.Battery >= 100)
                                myBL.ReleaseDroneFromCharging(id);
                            Thread.Sleep(timer);
                        }
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

        private void checkStatusParcelOfDelivery(int id)
        {

        }

        private void DoContinueDelivery(int id)
        {

        }
    }
}
