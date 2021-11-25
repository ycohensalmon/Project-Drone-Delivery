﻿using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategory MaxWeight { get; set; }
            public override string ToString()
            {
                return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}";
            }
        }
    }
}