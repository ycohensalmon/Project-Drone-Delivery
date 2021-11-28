using System;
using System.Collections.Generic;
using IBL.BO;
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
        /// <param name="bl">the parameter that include all the lists</param>
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
        /// <param name="bl">the parameter that include all the lists</param>
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
        /// <param name="bl">the parameter that include all the lists</param>
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
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintStations(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Station> stations = bl.GetStations();

            foreach (var item in stations){ Console.WriteLine(bl.GetStationById(item.Id)); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintDrones(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Drone> drones = bl.GetDrones();

            foreach (var item in drones)
            {
                Console.WriteLine(bl.GetDroneById(item.Id));
            }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintCostumers(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Customer> customers = bl.GetCustomers();
            
            foreach (var item in customers)
            {
                Console.WriteLine(bl.GetCustomerById(item.Id));
            }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintParcels(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Parcel> parcels = bl.GetParcels();
            
            foreach (var item in parcels)
            {
                Console.WriteLine(bl.GetParcelById(item.Id));
            }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintParcelsWithoutDrone(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Parcel> parcels = bl.GetParcelsWithoutDrone();

            foreach (var item in parcels)
            {
                Console.WriteLine(bl.GetParcelById(item.Id));
            }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        /// <param name="bl">the parameter that include all the lists</param>
        private static void PrintStationWithChargeSolts(IBL.IBL bl)
        {
            IEnumerable<IDAL.DO.Station> stations = bl.GetStationWithChargeSolts();

            foreach (var item in stations)
            {
                Console.WriteLine(bl.GetStationById(item.Id));
            }
        }
    }
}
