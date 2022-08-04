using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Utils
    {
        //bonus methods to display sexasegimal coordination and find distance between ocations
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

        public static string GetHashPassword(string password)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
