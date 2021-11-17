using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            private double longitude;
            private double lattitude;

            public double Longitude { get => longitude; set => longitude = value; }
            public double Lattitude { get => lattitude; set => lattitude = value; }

            public override string ToString() => $"Longitude: {Longitude}, Lattitude:{Lattitude}";

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
