using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneCharge
        {
            private int droneId;
            private int stationId;

            public int DroneId { get => droneId; set => droneId = value; }
            public int StationId { get => stationId; set => stationId = value; }
            public override string ToString()
            {
                return string.Format($"Drone Id: {DroneId},  Station Id: {StationId}");
            }
        }
    }
}
