using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace BO
    {
        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public override string ToString() => $"Latitude: {Distance.Sexagesimal(Latitude, 'N')}\nLongitude:{Distance.Sexagesimal(Longitude, 'E')}";
        }
    }
}
