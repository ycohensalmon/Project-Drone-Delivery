using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public static class Distance
        {
            public static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
            {
                var R = 6371; // Radius of the earth in km
                var dLat = Deg2rad(lat2 - lat1); // deg2rad below
                var dLon = Deg2rad(lon2 - lon1);
                var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                var d = R * c; // Distance in km
                return d;
            }
            public static double Deg2rad(double deg) => deg * (Math.PI / 180);

            /// <summary>
            /// finds sexasegiamal value of latitude
            /// </summary>
            /// <param name="decimalDegree">Longitude or latitude</param>
            /// <param name="c">Air directions</param>
            /// <returns>string with the loation</returns>
            public static string Sexagesimal(double decimalDegree, char c)
            {
                // calculate secondes
                double latDegrees = decimalDegree;
                int latSecondes = (int)Math.Round(latDegrees * 60 * 60);
                // calculate gerees
                double latDegreeWithFraction = decimalDegree;
                int degrees = (int)latDegreeWithFraction;
                // claculate minutes
                double fractionalDegree = latDegrees - degrees;
                double minutesWithFraction = 60 * fractionalDegree;
                int minutes = (int)minutesWithFraction;
                // calculate seconde with fraction
                double fractionalMinutes = minutesWithFraction - minutes;
                double secondesWithFraction = 60 * fractionalMinutes;

                return $"{degrees}°{minutes}'{string.Format("{0:F3}", secondesWithFraction)}\"{c}";
            }
        }
    }
}
