﻿using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            private int id;
            private string model;
            private WeightCategory maxWeight;
            //private DroneStatuses status;
            //private double battery;

            public int Id { get => id; set => id = value; }
            public string Model { get => model; set => model = value; }
            public WeightCategory MaxWeight { get => maxWeight; set => maxWeight = value; }
            //public DroneStatuses Status { get => status; set => status = value; }
            //public double Battery { get => battery; set => battery = value; }
            public override string ToString()
            {
                return $"Id:{Id}, Model:{Model}, MaxWeight:{MaxWeight}";
            }
        }
    }
}