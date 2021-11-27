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
        public void NewDroneInList(DroneInList temp, int numStation);
        public void ConnectDroneToParcel(int droneId);
        public void CollectParcelsByDrone(int droneId);
        public void DeliveredParcel(int droneId);
        public void SendDroneToCharge(int droneId);
        public void UpdateDrone(int droneId, string model);
        public void ReleaseDroneFromCharging(int droneId, double timeCharge);
        public void UpdateBase(int num, string newName, string newChargeSolts);
        public void UpdateCustomer(int customerID, string newName, string newPhone);
        public Station GetStationById(int stationId);
        public DroneInList GetDroneById(int droneId);
        public Customer GetCustomerById(int customerId);
        public void PrintStations();
        public void PrintDrones();
        public void PrintCustomers();
        public void PrintParcels();
    }

}
