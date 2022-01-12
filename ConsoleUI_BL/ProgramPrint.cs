using System;
using System.Collections.Generic;
using BO;
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
        private static void PrintStationById()
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);
            Console.WriteLine(bl.GetStationById(stationId));
        }

        /// <summary>
        /// returns the object drone that matches the id
        /// </summary>
        private static void PrintDroneById()
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine(bl.GetDroneById(droneId));
        }

        /// <summary>
        /// returns the object customer that matches the id
        /// </summary>
        private static void PrintCustomerById()
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine(bl.GetCustomerById(customerId));
        }

        private static void PrintUserById(int userId)
        {
            Console.WriteLine(bl.GetParcelFromCustomer(userId));
            Console.WriteLine(bl.GetParcelToCustomer(userId));
        }

        private static void confirmPackage(int userId)
        {
            ///////////
            Console.WriteLine("choose the parcel to confirm (id)\n");
            var parcels = from x in bl.GetParcelToCustomer(userId)
                          where x.Status != ParcelStatuses.Delivered
                          select x;

            Console.WriteLine(parcels);
            int.TryParse(Console.ReadLine(), out int parcelId);

            bl.confirmPackage(userId, parcelId);
        }

        /// <summary>
        /// returns the object parcel that matches the id
        /// </summary>
        private static void PrintParcelById()
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
        private static void PrintStations()
        {
            foreach (var item in bl.GetStations()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        private static void PrintDrones()
        {
            foreach (var item in bl.GetDrones()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        private static void PrintCostumers()
        {
            foreach (var item in bl.GetCustomers()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        private static void PrintParcels()
        {
            foreach (var item in bl.GetParcels()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        private static void PrintParcelsWithoutDrone()
        {
            foreach (var item in bl.GetParcelsWithoutDrone()) { Console.WriteLine(item); }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        private static void PrintStationWithChargeSolts()
        {
            foreach (var item in bl.GetStationWithChargeSolts()) { Console.WriteLine(item); }
        }
    }
}
