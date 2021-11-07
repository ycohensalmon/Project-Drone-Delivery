using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneInParcel
        {
            private int id;
            private double battery;
            private Location location;
            public int Id { get => id; set => id = value; }
            public double Battery { get => battery; set => battery = value; }
            public Location Location { get => location; set => location = value; }

            public override string ToString()
            {
                return $"Id:{Id}, Battery:{Battery}, Location:{Location}";
            }
        }
    }
}
