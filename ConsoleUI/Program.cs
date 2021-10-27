using IDAL.DO;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = DalObject.DalObject.GetDrones().Count;

            DalObject.DalObject dal = new DalObject.DalObject();

            int choises = 0;
            Console.WriteLine(
                "Hello :-)\n\nThis is a project for deliveries by drones\n\n");

            PrintFirstMenu();

            do
            {

                int.TryParse(Console.ReadLine(), out choises);
                switch (choises)
                {
                    case 1:
                        PrintMenu1();

                        int.TryParse(Console.ReadLine(), out choises);
                        switch (choises)
                        {
                            case 1:
                                Console.WriteLine();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        PrintMenu2();
                        break;
                    case 3:
                        PrintMenu3();
                        break;
                    case 4:
                        PrintMenu4();

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
                                    if(DalObject.DalObject.GetIndexStation(i).ChargeSolts > 0)
                                        Console.WriteLine(DalObject.DalObject.GetIndexStation(i));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                PrintFirstMenu();
                int.TryParse(Console.ReadLine(), out choises);
            } while (choises != 5);
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
    }
}