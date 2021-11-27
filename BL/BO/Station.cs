using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int ChargeSolts { get; set; }
            public List<DroneCharge> DroneCharges { get; set; } = new();
            public override string ToString()
            {
                if(DroneCharges == null)
                    return string.Format($"Id: {Id}\nName: {Name}\nLocation: {Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges: 0");

                return string.Format($"Id: {Id}\nName: {Name}\nLocation: {Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges: {DroneCharges}");
            }
        }
    }
}
