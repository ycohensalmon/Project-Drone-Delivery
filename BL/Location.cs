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
            private string longitude;
            private string lattitude;

            public string Longitude { get => longitude; set => longitude = value; }
            public string Lattitude { get => lattitude; set => lattitude = value; }

            public override string ToString() => $"Longitude: {Longitude}, Lattitude:{Lattitude}";
        }
    }
}
