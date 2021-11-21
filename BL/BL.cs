using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;


namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public void NewStation(Station x)
            {
                throw new NotImplementedException();
            }

            public void NewCostumer(Customer x)
            {
                throw new NotImplementedException();
            }

            public void NewDrone(Drone x, int numStation)
            {
                throw new NotImplementedException();
            }

            public void NewParcel(Parcel x, int senderID, int receiveID)
            {
                throw new NotImplementedException();
            }

            public void sendDroneToCharge(int droneID, int baseStatiunID)
            {
                throw new NotImplementedException();
            }
        }
    }
}
