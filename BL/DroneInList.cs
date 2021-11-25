using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInList
        {
            public DroneInList(string model)
            {
                this.Model = model;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategory MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public Location Location { get; set; }
            public int NumParcel { get; set; }
            public override string ToString()
            {
                return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}, Status:{Status}, Battery:{Battery}, Location:{Location}, numParcel:{NumParcel}";
            }
        }
    }
}
