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
            private double latitude;
            private double longitude;

            public double Latitude { get => latitude; set => latitude = value; }
            public double Longitude { get => longitude; set => longitude = value; }

            public override string ToString() => $"Latitude: {Distance.Sexagesimal(Latitude, 'N')}, Longitude:{Distance.Sexagesimal(Longitude, 'E')}";
        }
    }
}
