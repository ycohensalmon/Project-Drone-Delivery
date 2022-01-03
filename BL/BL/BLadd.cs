using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using BlApi;

namespace BL
{
    internal partial class BL : IBL
    {
        public void NewStation(Station station)
        {
            try
            {
                dalObj.NewStation(new DO.Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Latitude = station.Location.Latitude,
                    Longitude = station.Location.Longitude,
                    ChargeSolts = station.ChargeSolts,
                    IsDeleted = false
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

                dalObj.NewDrone(new DO.Drone
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (DO.WeightCategory)drone.MaxWeight,
                    IsDeleted = false
                });

                dalObj.SendDroneToBaseCharge(drone.Id, stationId);
                drones.Add(drone);
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
                dalObj.NewCostumer(new DO.Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Latitude = customer.Location.Latitude,
                    Longitude = customer.Location.Longitude,
                    IsDeleted = false
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
                HelpSerialNum = dalObj.NewParcel(new DO.Parcel
                {
                    Id = HelpSerialNum,
                    SenderId = senderID,
                    TargetId = receiveID,
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null,
                    Weight = (DO.WeightCategory)parcel.Weight,
                    Priorities = (DO.Priority)parcel.Priorities
                });
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }
    }
}
