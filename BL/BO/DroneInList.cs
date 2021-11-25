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
            private int id;
            private string model;
            private WeightCategory maxWeight;
            private double battery;
            private DroneStatuses status;
            private Location location;
            private int numParcel;

            public int Id { get => id; set => id = value; }
            public string Model { get => model; set => model = value; }
            public WeightCategory MaxWeight { get => maxWeight; set => maxWeight = value; }
            public double Battery { get => battery; set => battery = value; }
            public DroneStatuses Status { get => status; set => status = value; }
            public Location Location { get => location; set => location = value; }
            public int NumParcel { get => numParcel; set => numParcel = value; }
            public override string ToString()
            {
                return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}, Status:{Status}, Battery:{Battery}, Location:{Location}, numParcel:{numParcel}";
            }
        }
    }
}
