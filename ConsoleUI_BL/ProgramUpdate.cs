using System;
using IBL.BO;
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

        private static void UpdateDrone(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);
            string model = AddModel(true);

            bl.UpdateDrone(droneId, model);
        }

        private static void UpdateBase(IBL.IBL bl)
        {
            int stationID = AddId4(false,true);
            Console.WriteLine("Enter the new name of the base (if you dont want change the name press Enter)");
            string newName = Console.ReadLine();
            string newChargeSolts = AddStringChargeSlot();

            bl.UpdateBase(stationID, newName, newChargeSolts);
        }

        private static void UpdateCustomer(IBL.IBL bl)
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
        private static void SendDroneToCharge(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.SendDroneToCharge(droneId);
        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);

            Console.WriteLine("Enter the charge time (in minute)\n");
            double.TryParse(Console.ReadLine(), out double timeCharge);

            bl.ReleaseDroneFromCharging(droneId, timeCharge);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AssociateDroneToParcel(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.ConnectDroneToParcel(droneId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.CollectParcelsByDrone(droneId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void DeliveredParcelToCostumer(IBL.IBL bl)
        {
            int droneId = AddId4(true,false);

            bl.DeliveredParcel(droneId);
        }
    }
}
