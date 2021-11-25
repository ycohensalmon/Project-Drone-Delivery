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
            private int id;
            private string name;
            private Location location;
            private int chargeSolts;
            private List<DroneCharge> droneCharges = new();

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public Location Location { get => location; set => location = value; }
            public int ChargeSolts { get => chargeSolts; set => chargeSolts = value; }
            public List<DroneCharge> DroneCharges { get => droneCharges; set => droneCharges = value; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}\nName: {Name}\nLocation: {Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges: {DroneCharges}");
            }
        }
    }
}
