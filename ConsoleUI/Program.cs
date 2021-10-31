using System;
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
            DalObject.DataSource dal = new();

            PrintEnterToTheProject();

            int choises = 0;

            do
            {
                Pause();
                PrintFirstMenu();
                int.TryParse(Console.ReadLine(), out choises);

                switch (choises)
                {
                    case 1:
                        PrintMenu1();
                        Switch1();
                        break;
                    case 2:
                        PrintMenu2();
                        Switch2();
                        break;
                    case 3:
                        PrintMenu3();
                        Switch3();
                        break;
                    case 4:
                        PrintMenu4();
                        Switch4();
                        break;
                    default:
                        break;
                }

            } while (choises != 5);
        }


        // print menus
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
        private static void PrintFirstMenu()
        {
            Console.WriteLine(
                "For add options of Drone, Customer, Parcel or Base station - press 1:\n" +
                "For update options - - - - - - - - - - - - - - - - - - - - - press 2:\n" +
                "For display options (all according to a suitable ID number)- press 3:\n" +
                "For options for displaying the lists - - - - - - - - - - - - press 4:\n" +
                "To exit from this project- - - - - - - - - - - - - - - - - - press 5:");
        }
        private static void PrintMenu1()
        {
            Console.WriteLine(
                "for adding a base station - - - - - - press 1\n" +
                "for adding a drone to the list- - - - press 2\n" +
                "for absorption of a new customer- - - press 3\n" +
                "for receiving a package for delivery- press 4");
        }
        private static void PrintMenu2()
        {
            Console.WriteLine(
                "Assigning a package to a skimmer- - - - - - - - - press 1\n" +
                "Collection of a package by drone- - - - - - - - - press 2\n" +
                "Delivery package to customer- - - - - - - - - - - press 3\n" +
                "Sending a skimmer for charging at a base station- press 4\n" +
                "Release skimmer from charging at base station - - press 5");
        }
        private static void PrintMenu3()
        {
            Console.WriteLine(
                "Base station view- press 1\n" +
                "Drone display- - - press 2\n" +
                "Customer view- - - press 3\n" +
                "Package view - - - press 4");
        }
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
        private static void Pause()
        {
            Console.WriteLine("press to continue...");
            Console.ReadKey();
            Console.Clear();
        }


        // switchs
        private static void Switch1()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation();
                    break;
                case 2:
                    AddDrone();
                    break;
                case 3:
                    AddCustomer();
                    break;
                case 4:
                    AddParcel();
                    break;
                default:
                    break;
            }
        }
        private static void Switch2()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AssociateDroneToParcel();
                    break;
                case 2:
                    CollectParcelsByDrones();
                    break;
                case 3:
                    deliveredParcelToCostumer();
                    break;
                case 4:
                    SendDroneToCharge();
                    break;
                case 5:
                    ReleaseDroneFromChargingBase();
                    break;
                default:
                    break;
            }
        }
        private static void Switch3()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStationById();
                    break;
                case 2:
                    PrintDroneById();
                    break;
                case 3:
                    PrintCustomerById();
                    break;
                case 4:
                    PrintParcelById();
                    break;
                default:
                    break;
            }
        }
        private static void Switch4()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStations();
                    break;
                case 2:
                    PrintDrones();
                    break;
                case 3:
                    PrintCostumers();
                    break;
                case 4:
                    PrintParcels();
                    break;
                case 5:
                    PrintParcelsWithoutDrone();
                    break;
                case 6:
                    PrintStationWithChargeSolts();
                    break;
                default:
                    break;
            }
        }


        // switch 1 - Adds fonctions
        private static void AddStation()
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

            DalObject.DalObject.NewStation(id, name, longitude, lattitude, chargeSlots);

        }
        private static void AddCustomer()
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

            DalObject.DalObject.NewCostumer(id, name, phone, longitude, lattitude);
        }
        private static void AddDrone()
        {
            int id, model, maxWeight;

            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("add model: for Cobra press 0, Nagic press 1, Mavic_Air press 2, DJI press 3, Mickcara press 4:\n");
            int.TryParse(Console.ReadLine(), out model);
            Console.WriteLine("chose the weightCategory\n");
            int.TryParse(Console.ReadLine(), out maxWeight);

            DalObject.DalObject.NewDrone(id, model, maxWeight);
        }
        private static void AddParcel()
        {
            int id, priorities, weight;

            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("choise the priority, for Normal press 0, Fast press 1, Emergency press 2\n");
            int.TryParse(Console.ReadLine(), out priorities);
            Console.WriteLine("chose the weight of the parcel, for Light press 0, Medium press 1, Heavy press 2\n");
            int.TryParse(Console.ReadLine(), out weight);

            DalObject.DalObject.NewParcel(id, priorities, weight);
        }


        // switch 2 - Updates fonctions
        private static void AssociateDroneToParcel()
        {
            int parcelId, droneId;
            List<Parcel> temp = DalObject.DalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId == 0){ Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            List<Drone> temp2 = DalObject.DalObject.GetDrones();
            foreach (Drone y in temp2) { if (y.Status == DroneStatuses.Available) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            DalObject.DalObject.ConnectDroneToParcel(droneId, parcelId);
        }
        private static void CollectParcelsByDrones()
        {

            int parcelId;
            List<Parcel> temp = DalObject.DalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.DroneId != 0) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            DalObject.DalObject.CollectParcelByDrone(parcelId);
        }
        private static void deliveredParcelToCostumer()
        {
            int parcelId;
            List<Parcel> temp = DalObject.DalObject.GetParcels();
            foreach (Parcel x in temp) { if (x.PickedUp != DateTime.MinValue && x.Delivered == DateTime.MinValue) { Console.WriteLine(x); } }
            Console.WriteLine("Enter the Id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);

            DalObject.DalObject.DeliveredParcel(parcelId);
        }
        private static void SendDroneToCharge()
        {
            int droneId, stationId;
            List<Drone> temp = DalObject.DalObject.GetDrones();
            foreach (Drone y in temp) { if (y.Status != DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone to send to charge");
            int.TryParse(Console.ReadLine(), out droneId);

            PrintStationWithChargeSolts();
            Console.WriteLine("Enter the Id of the station");
            int.TryParse(Console.ReadLine(), out stationId);

            DalObject.DalObject.SendDroneToBaseCharge(droneId, stationId);

        }
        private static void ReleaseDroneFromChargingBase()
        {
            int droneId;
            List<Drone> temp = DalObject.DalObject.GetDrones();
            foreach (Drone y in temp) { if (y.Status == DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            Console.WriteLine("Enter the Id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);

            DalObject.DalObject.ReleaseDroneFromCharging(droneId);


        }


        // switch 3 - print index in the list (by id)
        private static void PrintParcelById()
        {
            int parcelId;
            Console.WriteLine("enter the id of the parcel");
            int.TryParse(Console.ReadLine(), out parcelId);
            Console.WriteLine(DalObject.DalObject.GetParcelById(parcelId));
        }
        private static void PrintCustomerById()
        {
            int customerId;
            Console.WriteLine("enter the id of the customer");
            int.TryParse(Console.ReadLine(), out customerId);
            Console.WriteLine(DalObject.DalObject.GetCustomerById(customerId));
        }
        private static void PrintDroneById()
        {
            int droneId;
            Console.WriteLine("enter the id of the drone");
            int.TryParse(Console.ReadLine(), out droneId);
            Console.WriteLine(DalObject.DalObject.GetDroneById(droneId));
        }
        private static void PrintStationById()
        {
            int stationId;
            Console.WriteLine("enter the id of the station");
            int.TryParse(Console.ReadLine(), out stationId);
            Console.WriteLine(DalObject.DalObject.GetStationById(stationId));
        }


        // switch 4 - prints fonction
        private static void PrintParcels()
        {
            for (int i = 0; i < DalObject.DalObject.GetParcels().Count; i++)
                Console.WriteLine(DalObject.DalObject.GetIndexParcel(i));
        }
        private static void PrintCostumers()
        {
            for (int i = 0; i < DalObject.DalObject.GetCustomers().Count; i++)
                Console.WriteLine(DalObject.DalObject.GetIndexCustomer(i));
        }
        private static void PrintDrones()
        {
            for (int i = 0; i < DalObject.DalObject.GetDrones().Count; i++)
                Console.WriteLine(DalObject.DalObject.GetIndexDrone(i));
        }
        private static void PrintStations()
        {
            for (int i = 0; i < DalObject.DalObject.GetStations().Count; i++)
                Console.WriteLine(DalObject.DalObject.GetIndexStation(i));
        }
        private static void PrintStationWithChargeSolts()
        {
            for (int i = 0; i < DalObject.DalObject.GetStations().Count; i++)
                if (DalObject.DalObject.GetIndexStation(i).ChargeSolts > 0)
                    Console.WriteLine(DalObject.DalObject.GetIndexStation(i));
        }
        private static void PrintParcelsWithoutDrone()
        {
            for (int i = 0; i < DalObject.DalObject.GetParcels().Count; i++)
                if (DalObject.DalObject.GetIndexParcel(i).DroneId == 0)
                    Console.WriteLine(DalObject.DalObject.GetIndexParcel(i));
        }
    }
}