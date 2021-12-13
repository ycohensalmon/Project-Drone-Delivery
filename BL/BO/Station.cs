using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (DroneCharges.Count == 0)
                return string.Format($"Id: {Id}\nName: {Name}\nLocation:\n{Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges: 0\n--------------");

            string droneCharges = "";
            int i = 1;
            foreach (var item in DroneCharges) { droneCharges += $"{i++}: {item}\n"; }

            return string.Format($"Id: {Id}\nName: {Name}\nLocation:\n{Location}\nAvailable charging positions: {ChargeSolts}\nDroneCharges:\n{droneCharges}--------------");
        }
    }
}
