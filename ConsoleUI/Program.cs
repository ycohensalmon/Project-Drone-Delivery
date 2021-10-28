using IDAL.DO;
using System;

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
                PrintFirstMenu();

                int.TryParse(Console.ReadLine(), out choises);
                switch (choises)
                {
                    case 1:
                        PrintMenu1();
                        Func1();

                        Switch1();
                        return;
                    case 2:
                        PrintMenu2();
                        Switch2();
                        break;
                    case 3:
                        PrintMenu3();
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

        private static void Switch1()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation();
                    break;
                default:
                    break;
            }
        }

        private static void AddStation()
        {
            int id;
            double location;
            Station temp = new();
            Console.WriteLine("add Id:\n");
            int.TryParse(Console.ReadLine(), out id);
            temp.Id = id;
            Console.WriteLine("add name:\n");
            temp.Name = Console.ReadLine();
            Console.WriteLine("add longitude:\n");
            double.TryParse(Console.ReadLine(), out location);
            temp.Longitude = location;
            Console.WriteLine("add lattitude:\n");
            double.TryParse(Console.ReadLine(), out location);
            temp.Longitude = location;
            temp.ChargeSolts = 10;
            
        }
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
            Console.WriteLine("press to continue habibi...\nWhat do you wait :)");
            Console.ReadKey();
            Console.Clear();
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
                "for receiving a package for delivery- press 4\n");
        }
        private static void PrintMenu2()
        {
            Console.WriteLine(
                "Assigning a package to a skimmer- - - - - - - - - press 1\n" +
                "Collection of a package by drone- - - - - - - - - press 2\n" +
                "Delivery package to customer- - - - - - - - - - - press 3\n" +
                "Sending a skimmer for charging at a base station- press 4\n" +
                "Release skimmer from charging at base station - - press 5\n");
        }
        private static void PrintMenu3()
        {
            Console.WriteLine(
                "Base station view- press 1\n" +
                "Drone display- - - press 2\n" +
                "Customer view- - - press 3\n" +
                "Package view - - - press 4\n");
        }
        private static void PrintMenu4()
        {
            
            Console.WriteLine(
                "Displays a list of base stations - - - - - - - - - - - - - - - - - - - - - press 1\n" +
                "Displays the list of drones- - - - - - - - - - - - - - - - - - - - - - - - press 2\n" +
                "View the customer list - - - - - - - - - - - - - - - - - - - - - - - - - - press 3\n" +
                "Displays the list of packages- - - - - - - - - - - - - - - - - - - - - - - press 4\n" +
                "Displays a list of packages that have not yet been assigned to the glider- press 5\n" +
                "Display base stations with available charging stations - - - - - - - - - - press 6\n");
        }
        private static void Switch1()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation();
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
                    int droneId, parcelId;
                    Console.WriteLine("Enter the Id of the drone");
                    int.TryParse(Console.ReadLine(), out droneId);
                    Console.WriteLine("Enter the Id of the parcel");
                    int.TryParse(Console.ReadLine(), out parcelId);
                    ConnectDroneToParcel(droneId, parcelId);




                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
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
                    for (int i = 0; i < DalObject.DalObject.GetStations().Count; i++)
                        Console.WriteLine(DalObject.DalObject.GetIndexStation(i));
                    break;
                case 2:
                    for (int i = 0; i < DalObject.DalObject.GetDrones().Count; i++)
                        Console.WriteLine(DalObject.DalObject.GetIndexDrone(i));
                    break;
                case 3:
                    for (int i = 0; i < DalObject.DalObject.GetCustomers().Count; i++)
                        Console.WriteLine(DalObject.DalObject.GetIndexCustomer(i));
                    break;
                case 4:
                    for (int i = 0; i < DalObject.DalObject.GetParcels().Count; i++)
                        Console.WriteLine(DalObject.DalObject.GetIndexParcel(i));
                    break;
                case 5:
                    for (int i = 0; i < DalObject.DalObject.GetParcels().Count; i++)
                        if (DalObject.DalObject.GetIndexParcel(i).DroneId == DalObject.DalObject.GetIndexDrone(i).Id)
                            Console.WriteLine(DalObject.DalObject.GetIndexParcel(i));
                    break;
                case 6:
                    for (int i = 0; i < DalObject.DalObject.GetStations().Count; i++)
                        if (DalObject.DalObject.GetIndexStation(i).ChargeSolts > 0)
                            Console.WriteLine(DalObject.DalObject.GetIndexStation(i));
                    break;
                default:
                    break;
            }
        }
    }
}