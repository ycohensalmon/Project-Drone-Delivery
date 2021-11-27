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
            public void NewStation(Station station)
            {
                try
                {
                    dalObj.NewStation(new IDAL.DO.Station
                    {
                        Id = station.Id,
                        Name = station.Name,
                        Latitude = station.Location.Latitude,
                        Longitude = station.Location.Longitude,
                        ChargeSolts = station.ChargeSolts
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void NewDroneInList(DroneInList drone, int stationId)
            {
                if (dalObj.GetStations().FirstOrDefault(station => station.Id == stationId).Id != stationId)
                    throw new ItemNotFoundException(stationId, "Station");

                try
                {
                    drone.Battery = rand.Next(20, 40);
                    drone.Battery += rand.NextDouble();
                    drone.Status = (DroneStatuses)1;
                    drone.NumParcel = 0;
                    drone.Location = new Location
                    {
                        Latitude = dalObj.GetStationById(stationId).Latitude,
                        Longitude = dalObj.GetStationById(stationId).Longitude
                    };

                    drones.Add(drone);

                    dalObj.NewDrone(new IDAL.DO.Drone
                    {
                        Id = drone.Id,
                        Model = drone.Model,
                        MaxWeight = (IDAL.DO.WeightCategory)drone.MaxWeight
                    });
                    dalObj.SendDroneToBaseCharge(drone.Id, stationId);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
                
            }

            public void NewCostumer(Customer customer)
            {
                try
                {
                    dalObj.NewCostumer(new IDAL.DO.Customer
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Latitude = customer.Location.Latitude,
                        Longitude = customer.Location.Longitude
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }

            public void NewParcel(Parcel parcel, int senderID, int receiveID)
            {
                if (senderID == receiveID)
                    throw new SelfDeliveryException();
                try
                {
                    dalObj.NewParcel(new IDAL.DO.Parcel
                    {
                        Id = DalObject.DataSource.SerialNum++,
                        SenderId = senderID,
                        TargetId = receiveID,
                        DroneId = 0,
                        Requested = DateTime.Now,
                        Scheduled = DateTime.MinValue,
                        PickedUp = DateTime.MinValue,
                        Delivered = DateTime.MinValue,
                        Weight = (IDAL.DO.WeightCategory)parcel.Weight,
                        Priorities = (IDAL.DO.Priority)parcel.Priorities
                    });
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }
        }
    }
}
