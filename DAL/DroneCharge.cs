using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            private int droneId;
            private int stationId;

            public int DroneId { get => droneId; set => droneId = value; }
            public int StationId { get => stationId; set => stationId = value; }
            public override string ToString()
            {
                return string.Format("the drone id is: {0}\nthe station id is: {1}",DroneId,StationId);
            }
        }
    }
}
