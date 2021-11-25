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
            int droneId = AddId4(true);
            string model = AddModel(true);

            bl.UpdateDrone(droneId, model);
        }

        private static void UpdateBase(IBL.IBL bl)
        {
            Console.WriteLine("Enter the number of the base\n");
            int.TryParse(Console.ReadLine(), out int num);
            Console.WriteLine("Enter the new name of the base (if you dont want change name press Enter) \n");
            string newName = Console.ReadLine();
            Console.WriteLine("Enter the new ChargeSolts of the base (if you dont want change name press Enter) \n");
            string newChargeSolts = Console.ReadLine();

            bl.UpdateBase(num, newName, newChargeSolts);
        }

        private static void UpdateCustomer(IBL.IBL bl)
        {
            Console.WriteLine("Enter the number of the base\n");
            int.TryParse(Console.ReadLine(), out int id);
            //isnt finished
        }

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void SendDroneToCharge(IBL.IBL bl)
        {
            int droneId = AddId4(true);

            bl.SendDroneToCharge(droneId);
        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(IBL.IBL bl)
        {
            int droneId = AddId4(true);

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
            int droneId = AddId4(true);

            bl.ConnectDroneToParcel(droneId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(IBL.IBL bl)
        {
            int droneId = AddId4(true);

            bl.CollectParcelsByDrone(droneId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void DeliveredParcelToCostumer(IBL.IBL bl)
        {
            int droneId = AddId4(true);

            bl.DeliveredParcel(droneId);
        }

    }
}
