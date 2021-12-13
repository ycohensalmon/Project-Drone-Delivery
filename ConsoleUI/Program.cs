using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using DO;
using System.Runtime.Serialization;
using DalFacade;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintEnterToTheProject();

            int choises = 0;

            IDal dal = DalFactory.GetDal();

            do
            {
                Pause();

                PrintFirstMenu();
                int.TryParse(Console.ReadLine(), out choises);
                try
                {
                    switch (choises)
                    {
                        case 1:
                            PrintMenu1();
                            Switch1(dal);
                            break;
                        case 2:
                            PrintMenu2();
                            Switch2(dal);
                            break;
                        case 3:
                            PrintMenu3();
                            Switch3(dal);
                            break;
                        case 4:
                            PrintMenu4();
                            Switch4(dal);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {

                    PrintException(e);
                }

            } while (choises != 5);
        }

        //-----------------------------------------------------------------------------------------------------------//
        // Print menu //
        //-----------------------------------------------------------------------------------------------------------//
        private static void PrintEnterToTheProject()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            int leftOffSet = (Console.WindowWidth / 2);
            Console.SetCursorPosition(leftOffSet - 4, 0);
            Console.WriteLine("Hello :-)\n\n");
            Console.SetCursorPosition(leftOffSet - 21, 1);
            Console.WriteLine("This is a project for deliveries by drones\n\n");
            Console.SetCursorPosition(leftOffSet - 15, 2);
            Console.WriteLine("Created by Elhanan and Yossef\n\n");
            Console.ResetColor();
        }

        private static void PrintException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// Print main menu to user
        /// </summary>
        private static void PrintFirstMenu()
        {
            Console.WriteLine(
                "For add options of Drone, Customer, Parcel or Base station - press 1:\n" +
                "For update options - - - - - - - - - - - - - - - - - - - - - press 2:\n" +
                "For display options (all according to a suitable ID number)- press 3:\n" +
                "For options for displaying the lists - - - - - - - - - - - - press 4:\n" +
                "To exit from this project- - - - - - - - - - - - - - - - - - press 5:");
        }

        /// <summary>
        /// Print add menu to user
        /// </summary>
        private static void PrintMenu1()
        {
            Console.WriteLine(
                "for adding a base station - - - - - - press 1\n" +
                "for adding a drone to the list- - - - press 2\n" +
                "for absorption of a new customer- - - press 3\n" +
                "for receiving a package for delivery- press 4");
        }

        /// <summary>
        /// Print the update menu to the user
        /// </summary>
        private static void PrintMenu2()
        {
            Console.WriteLine(
                "Assigning a package to a skimmer- - - - - - - - - press 1\n" +
                "Collection of a package by drone- - - - - - - - - press 2\n" +
                "Delivery package to customer- - - - - - - - - - - press 3\n" +
                "Sending a skimmer for charging at a base station- press 4\n" +
                "Release skimmer from charging at base station - - press 5");
        }

        /// <summary>
        /// Print the menu of display one to the user
        /// </summary>
        private static void PrintMenu3()
        {
            Console.WriteLine(
                "Base station view- press 1\n" +
                "Drone display- - - press 2\n" +
                "Customer view- - - press 3\n" +
                "Package view - - - press 4");
        }

        /// <summary>
        /// Print the menu of list displayy to user
        /// </summary>
        private static void PrintMenu4()
        {

            Console.WriteLine(
                "Displays a list of base stations - - - - - - - - - - - - - - - - - - - - - press 1\n" +
                "Displays the list of drones- - - - - - - - - - - - - - - - - - - - - - - - press 2\n" +
                "View the customer list - - - - - - - - - - - - - - - - - - - - - - - - - - press 3\n" +
                "Displays the list of packages- - - - - - - - - - - - - - - - - - - - - - - press 4\n" +
                "Displays a list of packages that have not yet been assigned to the glider- press 5\n" +
                "Display base stations with available charging stations - - - - - - - - - - press 6");
        }

        /// <summary>
        /// press to continue
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("press to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        //-----------------------------------------------------------------------------------------------------------//
        // Switchs //
        //-----------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// options of switch1 
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void Switch1(IDal dal)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation(dal);
                    break;
                case 2:
                    AddDrone(dal);
                    break;
                case 3:
                    AddCustomer(dal);
                    break;
                case 4:
                    AddParcel(dal);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch2
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void Switch2(IDal dal)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AssociateDroneToParcel(dal);
                    break;
                case 2:
                    CollectParcelsByDrones(dal);
                    break;
                case 3:
                    deliveredParcelToCostumer(dal);
                    break;
                case 4:
                    SendDroneToCharge(dal);
                    break;
                case 5:
                    ReleaseDroneFromChargingBase(dal);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch3
        /// </summary>
        /// <param name="dal">the parameter that include all the lists</param>
        private static void Switch3(IDal dal)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);

            switch (choises)
            {
                case 1:
                    PrintStationById(dal);
                    break;
                case 2:
                    PrintDroneById(dal);
                    break;
                case 3:
                    PrintCustomerById(dal);
                    break;
                case 4:
                    PrintParcelById(dal);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch4
        /// </summary>
        /// <param name="dal">the parameter that include all the lists</param>
        private static void Switch4(IDal dal)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);

            switch (choises)
            {
                case 1:
                    PrintStations(dal);
                    break;
                case 2:
                    PrintDrones(dal);
                    break;
                case 3:
                    PrintCostumers(dal);
                    break;
                case 4:
                    PrintParcels(dal);
                    break;
                case 5:
                    PrintParcelsWithoutDrone(dal);
                    break;
                case 6:
                    PrintStationWithChargeSolts(dal);
                    break;
                default:
                    break;
            }
        }


        //-----------------------------------------------------------------------------------------------------------//
        // switch 1 - Adds fonctions //
        //-----------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// add stetion to the list 
        /// </summary>
        /// <param name="dal">the parameter that include all the lists </param>
        private static void AddStation(IDal dal)
        {
            int id, chargeSlots;
            string name;
            double longitude;
            double lattitude;
            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("add name:\n");
            name = Console.ReadLine();
            Console.WriteLine("add longitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out longitude);
            Console.WriteLine("add lattitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out lattitude);
            Console.WriteLine("add chargeSolts:\n");
            int.TryParse(Console.ReadLine(), out chargeSlots);
            Station temp = new Station
            {
                Id = id,
                Name = name,
                Latitude = lattitude,
                Longitude = longitude,
                ChargeSolts = chargeSlots
            };
            dal.NewStation(temp);
        }

        /// <summary>
        /// add Customer to the list
        /// </summary>
        /// <param name="dal">the parameter that include all the lists</param>
        private static void AddCustomer(IDal dal)
        {
            int id, phone;
            string name;
            double longitude, lattitude;

            Console.WriteLine("add Id:\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("add name:\n");
            name = Console.ReadLine();
            Console.WriteLine("add Phone:\n");
            int.TryParse(Console.ReadLine(), out phone);
            Console.WriteLine("add longitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out longitude);
            Console.WriteLine("add lattitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out lattitude);

            Customer temp = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Latitude = lattitude,
                Longitude = longitude
            };

            dal.NewCostumer(temp);
        }

        /// <summary>
        /// add drone to the list
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void AddDrone(IDal Dal)
        {
            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("add mùodel:\n");
            string model = Console.ReadLine();
            Console.WriteLine("chose the weightCategory\n");
            int.TryParse(Console.ReadLine(), out int maxWeight);

            Drone temp = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategory)maxWeight
            };

            Dal.NewDrone(temp);
        }
        /// <summary>
        /// add parcel to the list
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void AddParcel(IDal Dal)
        {
            int priorities, weight;

            Console.WriteLine("choise the priority, for Normal press 0, Fast press 1, Emergency press 2\n");
            int.TryParse(Console.ReadLine(), out priorities);
            Console.WriteLine("chose the weight of the parcel, for Light press 0, Medium press 1, Heavy press 2\n");
            int.TryParse(Console.ReadLine(), out weight);

            Random rand = new Random();
            Parcel temp = new Parcel
            {
                Id = Dal.DataSource.SerialNum++,
                DroneId = 0,
                SenderId = rand.Next(10000, 99999),
                TargetId = rand.Next(10000, 99999),
                Requested = DateTime.Now,
                Scheduled = null,
                PickedUp = null,
                Delivered = null,
                Weight = (WeightCategory)weight,
                Priorities = (Priority)priorities
            };

            Dal.NewParcel(temp);
        }

        //-----------------------------------------------------------------------------------------------------------//
        // switch 2 - Updates fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void AssociateDroneToParcel(IDal Dal)
        {
            IEnumerable<Parcel> temp = Dal.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId == 0) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out int parcelId);

            IEnumerable<Drone> temp2 = Dal.GetDrones();
            //foreach (Drone y in temp2) { if (y.Status == DroneStatuses.Available) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out int droneId);

            Dal.ConnectDroneToParcel(droneId, parcelId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(IDal Dal)
        {

            int parcelId;
            IEnumerable<Parcel> temp = Dal.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId != 0) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            Dal.CollectParcelByDrone(parcelId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void deliveredParcelToCostumer(IDal Dal)
        {
            int parcelId;
            IEnumerable<Parcel> temp = Dal.GetParcels();
            foreach (Parcel x in temp) { if (x.PickedUp != null && x.Delivered == null) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            Dal.DeliveredParcel(parcelId);
        }

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void SendDroneToCharge(IDal Dal)
        {
            int droneId, stationId;
            IEnumerable<Drone> temp = Dal.GetDrones();
            //foreach (Drone y in temp) { if (y.Status != DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone to send to charge");
            int.TryParse(Console.ReadLine(), out droneId);

            PrintStationWithChargeSolts(Dal);
            Console.WriteLine("Enter the Id of the station");
            int.TryParse(Console.ReadLine(), out stationId);

            Dal.SendDroneToBaseCharge(droneId, stationId);

        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(IDal Dal)
        {
            int droneId;
            IEnumerable<Drone> temp = Dal.GetDrones();
            //foreach (Drone y in temp) { if (y.Status == DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            Dal.ReleaseDroneFromCharging(droneId);
        }

        //-----------------------------------------------------------------------------------------------------------//
        // switch 3 - print index in the list (by id) //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// returns the object Station that matches the id
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintStationById(IDal Dal)
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);

            Console.WriteLine(Dal.GetStationById(stationId));
        }

        /// <summary>
        /// returns the object drone that matches the id
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintDroneById(IDal Dal)
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            Console.WriteLine(Dal.GetDroneById(droneId));
        }

        /// <summary>
        /// returns the object customer that matches the id
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintCustomerById(IDal Dal)
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);

            Console.WriteLine(Dal.GetCustomerById(customerId));
        }

        /// <summary>
        /// returns the object parcel that matches the id
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintParcelById(IDal Dal)
        {
            int parcelId;
            Console.WriteLine("enter the id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            Console.WriteLine(Dal.GetParcelById(parcelId));
        }

        //-----------------------------------------------------------------------------------------------------------//
        // switch 4 - prints fonction //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// print the list of stations
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintStations(IDal Dal)
        {
            IEnumerable<Station> temp = Dal.GetStations();
            foreach (Station y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintDrones(IDal Dal)
        {
            IEnumerable<Drone> temp = Dal.GetDrones();
            foreach (Drone y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintCostumers(IDal Dal)
        {
            IEnumerable<Customer> temp = Dal.GetCustomers();
            foreach (Customer y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintParcels(IDal Dal)
        {
            IEnumerable<Parcel> temp = Dal.GetParcels();
            foreach (Parcel y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintParcelsWithoutDrone(IDal Dal)
        {
            IEnumerable<Parcel> temp = Dal.GetParcels(x => x.DroneId == 0);
            foreach (Parcel y in temp) {Console.WriteLine(y); }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        /// <param name="Dal">the parameter that include all the lists</param>
        private static void PrintStationWithChargeSolts(IDal Dal)
        {
            IEnumerable<Station> temp = Dal.GetStations(x => x.ChargeSolts != 0);
            foreach (Station y in temp) { Console.WriteLine(y); }
        }
    }
}