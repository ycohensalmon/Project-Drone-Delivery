using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;


namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public void NewStation(Station x)
            {
                try
                {
                    dalObj.NewStation(new IDAL.DO.Station
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude,
                        ChargeSolts = x.ChargeSolts
                    });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            public void NewCostumer(Customer x)
            {
                try
                {
                    dalObj.NewCostumer(new IDAL.DO.Customer
                    {
                        Id = x.Id,
                        Name =  x.Name,
                        Phone = x.Phone,
                        Latitude = x.Location.Latitude,
                        Longitude = x.Location.Longitude
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            public void NewDrone(Drone x, int numStation)
            {
                x.Battery = rand.Next(20,40);
                x.Battery += rand.NextDouble();
                x.Status = (DroneStatuses)1;
                x.Location = new Location
                {
                    Latitude = dalObj.GetStationById(numStation).Latitude,
                    Longitude = dalObj.GetStationById(numStation).Longitude
                };

                //do we need update the Drone charge list naw?

                drones.Add(x);

                dalObj.NewDrone(new IDAL.DO.Drone
                {
                    Id = x.Id,
                    Model = x.Model,
                    MaxWeight = (IDAL.DO.WeightCategory)x.MaxWeight
                });                                                                        
            }

            public void NewParcel(Parcel x, int senderID, int receiveID)
            {
                throw new NotImplementedException();
            }

            public void sendDroneToCharge(int droneID, int baseStatiunID)
            {
                throw new NotImplementedException();
            }
        }
    }
}
