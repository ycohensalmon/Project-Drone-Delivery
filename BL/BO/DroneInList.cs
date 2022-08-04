using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInList
    {
        public string Image { get; set; }
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location Location { get; set; }
        public int NumParcel { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}\nModel:{Model}\nMaxWeight:{MaxWeight}\nStatus:{Status}\nBattery:{Battery}\nLocation:\n{Location}\nnumParcel:{NumParcel}\n";
        }
    }
}
