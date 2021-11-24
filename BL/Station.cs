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
            private int chargeSolts;
            private List<DroneCharge> droneCharges = new();

            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int ChargeSolts { get => chargeSolts; set => chargeSolts = value; }
            public List<DroneCharge> DroneCharges { get => droneCharges; set => droneCharges = value; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}\nName: {Name}\nLocation: {Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges: {DroneCharges}");
            }
        }
    }
}
