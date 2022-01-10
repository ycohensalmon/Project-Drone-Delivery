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
                                    myBL.ConnectDroneToParcel(d.Id);
                                }
                            }
                            catch (NotEnoughBatteryException)
                            {
                                lock (myBL)
                                {
                                    myBL.SendDroneToCharge(d.Id);
                                }
                            }
                            catch (NoParcelException)
                            {

                                throw;
                            }
                           
                            break;
                        case DroneStatuses.Maintenance:
                            break;
                        case DroneStatuses.Delivery:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
