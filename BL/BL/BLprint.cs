using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                if (drone.Id != droneId)
                    throw new ItemNotFoundException(droneId, "DroneInList");
                return drone;
            }
        }
    }
}
