using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "Hello :-)\n\nThis is a project for deliveries by drones\n\n" +
                "For add options of Drone, Customer, Parcel or Base station - press 1:\n" +
                "For update options - - - - - - - - - - - - - - - - - - - - - press 2:\n" +
                "For display options (all according to a suitable ID number)- press 3:\n" +
                "For options for displaying the lists - - - - - - - - - - - - press 4:\n" +
                "To exit from this project- - - - - - - - - - - - - - - - - - press 5:");

            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            ;
            switch (choises)
            {
                case 1:
                    Console.WriteLine(
                        "for adding a base station - - - - - - press 1\n" +
                        "for adding a drone to the list- - - - press 2\n" +
                        "for absorption of a new customer- - - press 3\n" +
                        "for receiving a package for delivery- press 4\n");
                    break;
                case 2:
                    Console.WriteLine(
                        "Assigning a package to a skimmer- - - - - - - - - press 1\n" +
                        "Collection of a package by drone- - - - - - - - - press 2\n" +
                        "Delivery package to customer- - - - - - - - - - - press 3\n" +
                        "Sending a skimmer for charging at a base station- press 4\n" +
                        "Release skimmer from charging at base station - - press 5\n");
                    break;
                case 3:
                    Console.WriteLine(
                        "Base station view- press 1\n" +
                        "Drone display- - - press 2\n" +
                        "Customer view- - - press 3\n" +
                        "Package view - - - press 4\n");
                    break;
                case 4:
                    Console.WriteLine(
                        "Displays a list of base stations - - - - - - - - - - - - - - - - - - - - - press 1\n" +
                        "Displays the list of drones- - - - - - - - - - - - - - - - - - - - - - - - press 2\n" +
                        "View the customer list - - - - - - - - - - - - - - - - - - - - - - - - - - press 3\n" +
                        "Displays the list of packages- - - - - - - - - - - - - - - - - - - - - - - press 4\n" +
                        "Displays a list of packages that have not yet been assigned to the glider- press 5\n" +
                        "Display base stations with available charging stations - - - - - - - - - - press 6\n");
                    break;
                default:
                    break;
            }

            //IDAL.DO.BaseStation
            Console.WriteLine("Hello World!");
        }
    }
}
