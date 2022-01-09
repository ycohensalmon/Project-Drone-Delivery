using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using BO;
using BlApi;

namespace BL
{
    internal partial class BL : IBL
    {
        private double BatteryIossAvailable(double lat1, double lon1, double lat2, double lon2)
            => (Distance.GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2)) * Available;

        private double BatteryIossWithParcel(double lat1, double lon1, double lat2, double lon2, int Weight)
        {
            double batteryIoss = Distance.GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2);
            switch (Weight)
            {
                case 0:
                    batteryIoss *= LightParcel;
                    break;
                case 1:
                    batteryIoss *= MediumParcel;
                    break;
                case 2:
                    batteryIoss *= HeavyParcel;
                    break;
            }
            return batteryIoss;
        }

        private int GetNumParcel(DroneStatuses statuses, int droneId, IEnumerable<DO.Parcel> parcel)
        {
            if (statuses == DroneStatuses.Delivery)
            {
                DO.Parcel tempParcel = GetTempParcel(droneId, parcel);
                return tempParcel.Id;
            }
            else
                return 0;
        }

        private Location GetLocation(DroneStatuses statuses, int droneId, IEnumerable<DO.Parcel> parcel, IEnumerable<DO.Customer> customer, IEnumerable<DO.Station> station)
        {
            DO.Parcel tempParcel = GetTempParcel(droneId, parcel);

            if (statuses == DroneStatuses.Delivery)
            {
                // החבילה שויכה ולא נאספה
                if (tempParcel.Scheduled != null && tempParcel.PickedUp == null)
                {
                    // מיקום - תחנה הקרובה לשולח
                    DO.Customer tempCustomer = GetTempCustomer(customer, tempParcel);
                    return GetLocationWithMinDistance(station, tempCustomer);
                }
                // חבילה נאספה אך לא סופקה
                else if (tempParcel.PickedUp != null && tempParcel.Delivered == null)
                {
                    // מיקום - מיקום השולח
                    DO.Customer tempCustomer = GetTempCustomer(customer, tempParcel);
                    return GetLocationCustomer(tempCustomer);
                }
            }

            if (statuses == DroneStatuses.Maintenance)
            {
                int randIndexStation;
                do
                {
                    randIndexStation = rand.Next(station.Count());
                } while (station.ElementAt(randIndexStation).ChargeSolts == 0);

                int stasionID = station.ElementAt(randIndexStation).Id;
                dalObj.SendDroneToBaseCharge(droneId, stasionID);

                return GetLocationStation(station, stasionID);
            }

            if (statuses == DroneStatuses.Available)
            {
                List<Location> locations = new();
                foreach (var item in parcel) if (item.Delivered != null)
                    {
                        locations.Add(GetCustomerById(item.TargetId).Location);
                    }
                if (locations.Count != 0)
                {
                    return locations[rand.Next(locations.Count())];
                }
            }
            Location location = new();
            return location;
        }

        private Location GetLocationStation(IEnumerable<DO.Station> station, int stationID)
        {
            return new Location
            {
                Latitude = dalObj.GetStationById(stationID).Latitude,
                Longitude = dalObj.GetStationById(stationID).Longitude
            };
        }

        private Location GetLocationWithMinDistance(IEnumerable<DO.Station> station, DO.Customer tempCustomer)
        {
            double tempDistance, min = Distance.GetDistanceFromLatLonInKm(tempCustomer.Latitude, tempCustomer.Longitude, station.First().Latitude, station.First().Longitude);
            Location location = new() { Latitude = station.First().Latitude, Longitude = station.First().Longitude };

            foreach (var item in station)
            {
                tempDistance = Distance.GetDistanceFromLatLonInKm(tempCustomer.Latitude, tempCustomer.Longitude, item.Latitude, item.Longitude);

                if (min > tempDistance)
                {
                    min = tempDistance;
                    location.Latitude = item.Latitude;
                    location.Longitude = item.Longitude;
                }
            }
            return location;
        }

        private Location GetLocationCustomer(DO.Customer tempCustomer)
        {
            return new Location
            {
                Latitude = tempCustomer.Latitude,
                Longitude = tempCustomer.Longitude
            };
        }

        private double GetBattery(DroneStatuses status)
        {
            if (status != DroneStatuses.Maintenance) return rand.Next(30, 101);
            else return rand.Next(0, 20);
        }

        private DroneStatuses GetStatus(int droneId, IEnumerable<DO.Parcel> parcel)
        {
            if (GetTempParcel(droneId, parcel).DroneId == droneId && GetTempParcel(droneId, parcel).Delivered == null)
                return DroneStatuses.Delivery;
            else
                return (DroneStatuses)rand.Next(2);
        }

        private DO.Customer GetTempCustomer(IEnumerable<DO.Customer> customer, DO.Parcel tempParcel)
        {
            return customer.FirstOrDefault(customer => customer.Id == tempParcel.SenderId);
        }

        private DO.Parcel GetTempParcel(int droneId, IEnumerable<DO.Parcel> parcel)
        {
            return parcel.FirstOrDefault(parcel => parcel.DroneId == droneId);
        }

        private ParcelStatuses GetParcelStatus(BO.Parcel parcel)
        {
            if (parcel.Requested != null && parcel.Scheduled == null)
                return ParcelStatuses.Requested;

            if (parcel.Scheduled != null && parcel.PickedUp == null)
                return ParcelStatuses.Scheduled;

            if (parcel.PickedUp != null && parcel.Delivered == null)
                return ParcelStatuses.PickedUp;

            return ParcelStatuses.Delivered;
        }
    }
}
