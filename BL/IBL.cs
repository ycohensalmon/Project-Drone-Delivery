using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{

    public interface IBL 
    {
        public void NewStation(Station x);
        public void NewCostumer(Customer x);
        public void NewParcel(Parcel x, int senderID, int receiveID);
        public void sendDroneToCharge(int droneID, int baseStatiunID);
        void NewDroneInList(DroneInList temp, int numStation);
        public void connectDroneToParcel(int droneId);
        public void CollectParcelsByDrone(int droneId);
        public void deliveredParcel(int droneId);
        public DroneInList GetDroneById(int droneId);
    }

}
