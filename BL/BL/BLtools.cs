using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            private double BatteryIossAvailable(double lat1, double lon1, double lat2, double lon2)
            {
                return (Distance.GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2)) * Available;
            }

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

            private int GetNumParcel(DroneStatuses statuses, int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                if (statuses == DroneStatuses.Delivery)
                {
                    IDAL.DO.Parcel tempParcel = GetTempParcel(droneId, parcel);
                    return tempParcel.Id;
                }
                else
                    return 0;
            }

            private Location GetLocation(DroneStatuses statuses, int droneId, IEnumerable<IDAL.DO.Parcel> parcel, IEnumerable<IDAL.DO.Customer> customer, IEnumerable<IDAL.DO.Station> station)
            {
                IDAL.DO.Parcel tempParcel = GetTempParcel(droneId, parcel);

                if (tempParcel.DroneId == droneId)
                {
                    // החבילה שויכה ולא נאספה
                    if (tempParcel.Scheduled != DateTime.MinValue && tempParcel.PickedUp == DateTime.MinValue)
                    {
                        // מיקום - תחנה הקרובה לשולח
                        IDAL.DO.Customer tempCustomer = GetTempCustomer(customer, tempParcel);
                        return GetLocationWithMinDistance(station, tempCustomer);
                    }
                    // חבילה נאספה אך לא סופקה
                    else if (tempParcel.PickedUp != DateTime.MinValue && tempParcel.Delivered == DateTime.MinValue)
                    {
                        // מיקום - מיקום השולח
                        IDAL.DO.Customer tempCustomer = GetTempCustomer(customer, tempParcel);
                        return GetLocationCustomer(tempCustomer);
                    }
                }

                if (statuses == DroneStatuses.Maintenance)
                {
                    int randIndexStation = rand.Next(station.Count());
                    dalObj.SendDroneToBaseCharge(droneId, station.ElementAt(randIndexStation).Id);

                    return GetLocationStation(station, randIndexStation);
                }

                if (statuses == DroneStatuses.Available)
                {
                    List<IDAL.DO.Parcel> parcelDelivered = new();
                    foreach (var item in parcel) if (item.Delivered != DateTime.MinValue) { parcelDelivered.Add(item); }
                    int randIndexStation = rand.Next(parcelDelivered.Count());
                    IDAL.DO.Customer tempCustomer = customer.First(customer => customer.Id == parcelDelivered[randIndexStation].TargetId);

                    return GetLocationCustomer(tempCustomer);
                }
                Location location = new();
                return location;
            }

            private Location GetLocationStation(IEnumerable<IDAL.DO.Station> station, int randIndexStation)
            {
                return new Location
                {
                    Latitude = station.ElementAt(randIndexStation).Latitude,
                    Longitude = station.ElementAt(randIndexStation).Longitude
                };
            }

            private Location GetLocationWithMinDistance(IEnumerable<IDAL.DO.Station> station, IDAL.DO.Customer tempCustomer)
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

            private Location GetLocationCustomer(IDAL.DO.Customer tempCustomer)
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

            private DroneStatuses GetStatus(int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                if (GetTempParcel(droneId, parcel).DroneId == droneId)
                    return DroneStatuses.Delivery;
                else
                    return (DroneStatuses)rand.Next(2);
            }

            private IDAL.DO.Customer GetTempCustomer(IEnumerable<IDAL.DO.Customer> customer, IDAL.DO.Parcel tempParcel)
            {
                return customer.First(customer => customer.Id == tempParcel.SenderId);
            }

            private IDAL.DO.Parcel GetTempParcel(int droneId, IEnumerable<IDAL.DO.Parcel> parcel)
            {
                return parcel.First(parcel => parcel.DroneId == droneId);
            }
        }
    }
}
