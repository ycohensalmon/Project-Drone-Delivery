﻿using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintEnterToTheProject();

            int choises = 0;

            DalObject.DalObject dalObject = new DalObject.DalObject();
            do
            {
                Pause();

                PrintFirstMenu();
                int.TryParse(Console.ReadLine(), out choises);

                switch (choises)
                {
                    case 1:
                        PrintMenu1();
                        Switch1(dalObject);
                        break;
                    case 2:
                        PrintMenu2();
                        Switch2(dalObject);
                        break;
                    case 3:
                        PrintMenu3();
                        Switch3(dalObject);
                        break;
                    case 4:
                        PrintMenu4();
                        Switch4(dalObject);
                        break;
                    default:
                        break;
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
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch1(DalObject.DalObject dalObject)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation(dalObject);
                    break;
                case 2:
                    AddDrone(dalObject);
                    break;
                case 3:
                    AddCustomer(dalObject);
                    break;
                case 4:
                    AddParcel(dalObject);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch2
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch2(DalObject.DalObject dalObject)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AssociateDroneToParcel(dalObject);
                    break;
                case 2:
                    CollectParcelsByDrones(dalObject);
                    break;
                case 3:
                    deliveredParcelToCostumer(dalObject);
                    break;
                case 4:
                    SendDroneToCharge(dalObject);
                    break;
                case 5:
                    ReleaseDroneFromChargingBase(dalObject);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch3
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch3(DalObject.DalObject dalObject)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStationById(dalObject);
                    break;
                case 2:
                    PrintDroneById(dalObject);
                    break;
                case 3:
                    PrintCustomerById(dalObject);
                    break;
                case 4:
                    PrintParcelById(dalObject);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch4
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch4(DalObject.DalObject dalObject)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStations(dalObject);
                    break;
                case 2:
                    PrintDrones(dalObject);
                    break;
                case 3:
                    PrintCostumers(dalObject);
                    break;
                case 4:
                    PrintParcels(dalObject);
                    break;
                case 5:
                    PrintParcelsWithoutDrone(dalObject);
                    break;
                case 6:
                    PrintStationWithChargeSolts(dalObject);
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
        /// <param name="dalObject">the parameter that include all the lists </param>
        private static void AddStation(DalObject.DalObject dalObject)
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
                Longitude = DalObject.DataSource.Sexagesimal(longitude, 'N'),
                Lattitude = DalObject.DataSource.Sexagesimal(lattitude, 'E'),
                ChargeSolts = chargeSlots
            };

            dalObject.NewStation(temp);
        }

        /// <summary>
        /// add Customer to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddCustomer(DalObject.DalObject dalObject)
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
                Longitude = DalObject.DataSource.Sexagesimal(longitude, 'N'),
                Latittude = DalObject.DataSource.Sexagesimal(lattitude, 'E')
            };

            dalObject.NewCostumer(temp);
        }

        /// <summary>
        /// add drone to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddDrone(DalObject.DalObject dalObject)
        {
            int id, maxWeight;
            string model;
            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("add model:\n");
            model = Console.ReadLine();
            Console.WriteLine("chose the weightCategory\n");
            int.TryParse(Console.ReadLine(), out maxWeight);

            Drone temp =new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategory)maxWeight,
                //Status = DroneStatuses.Available,
                //Battery = 100
            };

            dalObject.NewDrone(temp);
        }

        /// <summary>
        /// add parcel to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddParcel(DalObject.DalObject dalObject)
        {
            int id, priorities, weight;

            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("choise the priority, for Normal press 0, Fast press 1, Emergency press 2\n");
            int.TryParse(Console.ReadLine(), out priorities);
            Console.WriteLine("chose the weight of the parcel, for Light press 0, Medium press 1, Heavy press 2\n");
            int.TryParse(Console.ReadLine(), out weight);

            Random rand = new Random();
            Parcel temp = new Parcel
            {
                Id = id,
                DroneId = 0,
                SenderId =  rand.Next(10000, 99999),
                TargetId = rand.Next(10000, 99999),
                Requested = DateTime.Now,
                Scheduled = DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Delivered = DateTime.MinValue,
                Weight = (WeightCategory)weight,
                Priorities = (Priority)priorities
            };

            dalObject.NewParcel(temp);
        }

        //-----------------------------------------------------------------------------------------------------------//
                                                // switch 2 - Updates fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AssociateDroneToParcel(DalObject.DalObject dalObject)
        {
            int parcelId, droneId;
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId == 0){ Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            List<Drone> temp2 = dalObject.GetDrones();
            foreach (Drone y in temp2) { if (y.Status == DroneStatuses.Available) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            dalObject.ConnectDroneToParcel(droneId, parcelId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(DalObject.DalObject dalObject)
        {

            int parcelId;
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId != 0) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            dalObject.CollectParcelByDrone(parcelId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void deliveredParcelToCostumer(DalObject.DalObject dalObject)
        {
            int parcelId;
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.PickedUp != DateTime.MinValue && x.Delivered == DateTime.MinValue) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            dalObject.DeliveredParcel(parcelId);
        }

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void SendDroneToCharge(DalObject.DalObject dalObject)
        {
            int droneId, stationId;
            IEnumerable<Drone> temp = dalObject.GetDrones();
            foreach (Drone y in temp) { if (y.Status != DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone to send to charge");
            int.TryParse(Console.ReadLine(), out droneId);

            PrintStationWithChargeSolts(dalObject);
            Console.WriteLine("Enter the Id of the station");
            int.TryParse(Console.ReadLine(), out stationId);

            dalObject.SendDroneToBaseCharge(droneId, stationId);

        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(DalObject.DalObject dalObject)
        {
            int droneId;
            List<Drone> temp = dalObject.GetDrones();
            foreach (Drone y in temp) { if (y.Status == DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            dalObject.ReleaseDroneFromCharging(droneId);


        }

        //-----------------------------------------------------------------------------------------------------------//
                                   // switch 3 - print index in the list (by id) //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// returns the object Station that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStationById(DalObject.DalObject dalObject)
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);
            Console.WriteLine(dalObject.GetStationById(stationId));
        }

        /// <summary>
        /// returns the object drone that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintDroneById(DalObject.DalObject dalObject)
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine(dalObject.GetDroneById(droneId));
        }

        /// <summary>
        /// returns the object customer that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintCustomerById(DalObject.DalObject dalObject)
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine(dalObject.GetCustomerById(customerId));
        }

        /// <summary>
        /// returns the object parcel that matches the id
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcelById(DalObject.DalObject dalObject)
        {
            int parcelId;
            Console.WriteLine("enter the id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);
            Console.WriteLine(dalObject.GetParcelById(parcelId));
        }

        //-----------------------------------------------------------------------------------------------------------//
                                             // switch 4 - prints fonction //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// print the list of stations
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStations(DalObject.DalObject dalObject)
        {
            List<Station> temp = dalObject.GetStations();
            foreach (Station y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Drones
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintDrones(DalObject.DalObject dalObject)
        {
            List<Drone> temp = dalObject.GetDrones();
            foreach (Drone y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Costumers
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintCostumers(DalObject.DalObject dalObject)
        {
            List<Customer> temp = dalObject.GetCustomers();
            foreach (Customer y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print the list of Parcels
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcels(DalObject.DalObject dalObject)
        {
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel y in temp) { Console.WriteLine(y); }
        }

        /// <summary>
        /// print a list with all the parcels that are not associated to a drone
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintParcelsWithoutDrone(DalObject.DalObject dalObject)
        {
            List<Parcel> temp = dalObject.GetParcels();
            foreach (Parcel y in temp) { if (y.DroneId == 0) { Console.WriteLine(y); } }
        }

        /// <summary>
        /// print an array with tyhe list of stations with empty charge slots
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void PrintStationWithChargeSolts(DalObject.DalObject dalObject)
        {
            List<Station> temp = dalObject.GetStations();
            foreach (Station y in temp) { if (y.ChargeSolts != 0) { Console.WriteLine(y); } }
        }
    }
}