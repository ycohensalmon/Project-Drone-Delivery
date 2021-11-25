using System;
using System.Collections.Generic;
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
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStationById(IBL.IBL bl)
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);
            Console.WriteLine(bl.GetStationById(stationId));
        }

        /// <summary>
        /// returns the object drone that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintDroneById(IBL.IBL bl)
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine(bl.GetDroneById(droneId));
        }

        /// <summary>
        /// returns the object customer that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintCustomerById(IBL.IBL bl)
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine(bl.GetCustomerById(customerId));
        }

        /// <summary>
        /// returns the object parcel that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcelById(IBL.IBL bl)
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
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStations(IBL.IBL bl)
        {
            List<Station> temp = bl.GetStations();
            foreach (Station y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintDrones(IBL.IBL bl)
        {
            List<Drone> temp = dalObject.GetDrones();
            foreach (Drone y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintCostumers(IBL.IBL bl)
        {
            List<Customer> temp = dalObject.GetCustomers();
            foreach (Customer y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcels(IBL.IBL bl)
        {
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcelsWithoutDrone(IBL.IBL bl)
        {
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel y in temp) { if (y.DroneId == 0) { Console.WriteLine(y); } }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStationWithChargeSolts(IBL.IBL bl)
        {
            List<Station> temp = dalObject.GetStations();
            foreach (Station y in temp) { if (y.ChargeSolts != 0) { Console.WriteLine(y); } }
        }
    }
}
