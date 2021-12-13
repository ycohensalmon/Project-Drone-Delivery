using System;
using BL.BO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        //-----------------------------------------------------------------------------------------------------------//
        // switch 2 - Updates fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        private static void UpdateDrone(BL.IBL bl)
        {
            int droneId = AddId4(true,false);
            string model = AddModel(true);

            bl.UpdateDrone(droneId, model);
        }

        private static void UpdateBase(BL.IBL bl)
        {
            int stationID = AddId4(false,true);
            Console.WriteLine("Enter the new name of the base (if you dont want change the name press Enter)");
            string newName = Console.ReadLine();
            string newChargeSolts = AddStringChargeSlot();

            bl.UpdateBase(stationID, newName, newChargeSolts);
        }

        private static void UpdateCustomer(BL.IBL bl)
        {
            Console.WriteLine("Enter the ID of the Customer\n");
            int.TryParse(Console.ReadLine(), out int CustomerID);
            Console.WriteLine("Enter the new name of the Customer (if you dont want change the name press Enter)");
            string newName = Console.ReadLine();
            Console.WriteLine("Enter the new phone of the Customer (if you dont want change the name press Enter)");
            string newPhone = Console.ReadLine();

            bl.UpdateCustomer(CustomerID, newName, newPhone);
        }

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void SendDroneToCharge(BL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.SendDroneToCharge(droneId);
        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(BL.IBL bl)
        {
            int droneId = AddId4(true,false);
            bl.ReleaseDroneFromCharging(droneId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AssociateDroneToParcel(BL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.ConnectDroneToParcel(droneId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(BL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.CollectParcelsByDrone(droneId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void DeliveredParcelToCostumer(BL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.DeliveredParcel(droneId);
        }
    }
}
