using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategory MaxWeight { get; set; }
            //public DroneStatuses Status { get => status; set => status = value; }
            //public double Battery { get => battery; set => battery = value; }
            public override string ToString()
            {
                return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}";
            }
        }
    }
}