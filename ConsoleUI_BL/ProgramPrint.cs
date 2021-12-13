using System;
using System.Collections.Generic;
using BL.BO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        //-----------------------------------------------------------------------------------------------------------//
        // switch 3 - print index in the list (by id) //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// returns the object Station that matches the id
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintStationById(BL.IBL bl)
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);
            Console.WriteLine(bl.GetStationById(stationId));
        }

        /// <summary>
        /// returns the object drone that matches the id
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintDroneById(BL.IBL bl)
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine(bl.GetDroneById(droneId));
        }

        /// <summary>
        /// returns the object customer that matches the id
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintCustomerById(BL.IBL bl)
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine(bl.GetCustomerById(customerId));
        }

        /// <summary>
        /// returns the object parcel that matches the id
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintParcelById(BL.IBL bl)
        {
            int parcelId;
            Console.WriteLine("enter the id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);
            Console.WriteLine(bl.GetParcelById(parcelId));
        }

        //-----------------------------------------------------------------------------------------------------------//
        // switch 4 - prints fonction //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// print the list of stations
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintStations(BL.IBL bl)
        {
            foreach (var item in bl.GetStations()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintDrones(BL.IBL bl)
        {
            foreach (var item in bl.GetDrones()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintCostumers(BL.IBL bl)
        {
            foreach (var item in bl.GetCustomers()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintParcels(BL.IBL bl)
        {
            foreach (var item in bl.GetParcels()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintParcelsWithoutDrone(BL.IBL bl)
        {
            foreach (var item in bl.GetParcelsWithoutDrone()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintStationWithChargeSolts(BL.IBL bl)
        {
            foreach (var item in bl.GetStationWithChargeSolts()) { Console.WriteLine(item); }
        }
    }
}
