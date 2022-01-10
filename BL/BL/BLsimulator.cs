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
        int timer = 500;
        double speed = 0.75;

        Drone d;

        public Simulator(int id, Action updateDelegate, Func<bool> stopDelegate, BL myBL)
        {
            lock (myBL)
            {
                d = myBL.GetDroneById(id);
            }

            while (!stopDelegate())
            {
                switch (d.Status)
                {
                    case DroneStatuses.Available:
                        try
                        {
                            lock (myBL)
                            {
                                myBL.ConnectDroneToParcel(id);
                            }
                        }
                        catch (NotEnoughBatteryException)
                        {
                            lock (myBL)
                            {
                                myBL.SendDroneToCharge(id);
                            }
                        }
                        catch (NoParcelException)
                        {
                            //wait
                        }
                        catch (ParcelTooHeavyException)
                        {
                            //stop
                        }
                        break;
                    case DroneStatuses.Maintenance:
                        lock (myBL)
                        {
                            if (checkFullBattery(id, myBL))
                                myBL.ReleaseDroneFromCharging(id);
                        }
                        break;
                    case DroneStatuses.Delivery:
                        checkStatusParcelOfDelivery(d.ParcelInTravel.Id);
                        DoContinueDelivery(d.ParcelInTravel.Id);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool checkFullBattery(int id, BL myBL)
        {
            lock (myBL)
            {
                var drone = myBL.dalObj.GetDroneCharges(x => x.DroneId == id).First();
                if ((DateTime.Now - drone.EnteryTime).Value.TotalSeconds * (myBL.getLoadingRate() / 60) >= 100)
                    return true;
                return false;
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
