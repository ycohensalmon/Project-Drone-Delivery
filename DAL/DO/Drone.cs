using System;


namespace DO
{
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategory MaxWeight { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}";
        }
    }
}
