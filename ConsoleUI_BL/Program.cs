using System;
using IBL.BO;

namespace ConsoleUI_BL
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintEnterToTheProject();

            int choises = 0;

            IBL.IBL bl = new BL(); 

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
                        Switch1(bl);
                        break;
                    case 2:
                        PrintMenu2();
                        Switch2(bl);
                        break;
                    case 3:
                        PrintMenu3();
                        Switch3(bl);
                        break;
                    case 4:
                        PrintMenu4();
                        Switch4(bl);
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
                "Assigning a package to a drone- - - - - - - - - press 1\n" +
                "Collection of a package by drone- - - - - - - - - press 2\n" +
                "Delivery package to customer- - - - - - - - - - - press 3\n" +
                "Sending a drone for charging at a base station- press 4\n" +
                "Release drone from charging at base station - - press 5\n" +
                "to update drone data (name only)- - - - - - - - press 6\n" +
                "to update base station data- - - - - - - - - -  press 7\n" +
                "to update customer data- - - - - - - - - - - -  press 8\n");
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
        private static void Switch1(IBL.IBL bl)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AddStation(bl);
                    break;
                case 2:
                    AddDrone(bl);
                    break;
                case 3:
                    AddCustomer(bl);
                    break;
                case 4:
                    AddParcel(bl);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch2
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch2(IBL.IBL bl)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    AssociateDroneToParcel(bl);
                    break;
                case 2:
                    CollectParcelsByDrones(bl);
                    break;
                case 3:
                    deliveredParcelToCostumer(bl);
                    break;
                case 4:
                    SendDroneToCharge(bl);
                    break;
                case 5:
                    ReleaseDroneFromChargingBase(bl);
                    break;
                case 6:
                    updatDrone(bl);
                    break;
                case 7:
                    updateBase(bl);
                    break;
                case 8:
                    updateCustomer(bl);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch3
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch3(IBL.IBL bl)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStationById(bl);
                    break;
                case 2:
                    PrintDroneById(bl);
                    break;
                case 3:
                    PrintCustomerById(bl);
                    break;
                case 4:
                    PrintParcelById(bl);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// options of switch4
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch4(IBL.IBL bl)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    PrintStations(bl);
                    break;
                case 2:
                    PrintDrones(bl);
                    break;
                case 3:
                    PrintCostumers(bl);
                    break;
                case 4:
                    PrintParcels(bl);
                    break;
                case 5:
                    PrintParcelsWithoutDrone(bl);
                    break;
                case 6:
                    PrintStationWithChargeSolts(bl);
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
        private static void AddStation(IBL.IBL bl)
        {
            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("add name:\n");
            string name = Console.ReadLine();
            Console.WriteLine("add longitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("add lattitude: (example 12.123456)\n");
            double.TryParse(Console.ReadLine(), out double lattitude);
            Console.WriteLine("add chargeSolts:\n");
            int.TryParse(Console.ReadLine(), out int chargeSlots);

            Location loc = new Location
            {
                Longitude = longitude,
                Latitude = lattitude
            };

            Station temp = new Station
            {
                Id = id,
                Name = name,
                Location = loc,
                ChargeSolts = chargeSlots
            };
            bl.NewStation(temp);
        }

        /// <summary>
        /// add Customer to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddCustomer(IBL.IBL bl)
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

            Location loc = new Location
            {
                Longitude = longitude,
                Latitude = lattitude
            };

            Customer temp = new Customer
            {
                Id = id,
                Name = name,
                Phone = phone,
                Location = loc
            };

            bl.NewCostumer(temp);
        }

        /// <summary>
        /// add drone to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddDrone(IBL.IBL bl)
        {
            int id, maxWeight, numStation;
            string model;
            Console.WriteLine("add Id: (4 digits)\n");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("add model:\n");
            model = Console.ReadLine();
            Console.WriteLine("add maximum weight that the drone can carry (1,2 or 3 KG)\n");
            int.TryParse(Console.ReadLine(), out maxWeight);
            Console.WriteLine("Select a station number for initial charging\n");
            int.TryParse(Console.ReadLine(), out numStation);

            Drone temp = new Drone
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategory)maxWeight-1,
            };

            bl.NewDrone(temp, numStation);
        }

        /// <summary>
        /// add parcel to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddParcel(IBL.IBL bl)
        {
            int senderID, receiveID, priorities, weight;

            Console.WriteLine("add Id of the sending customer: (9 digits)\n");
            int.TryParse(Console.ReadLine(), out senderID);
            Console.WriteLine("add Id of the receiving customer: (9 digits)\n");
            int.TryParse(Console.ReadLine(), out receiveID);
            Console.WriteLine("choise the priority, for Normal press 0, Fast press 1, Emergency press 2\n");
            int.TryParse(Console.ReadLine(), out priorities);
            Console.WriteLine("chose the weight of the parcel, for Light press 0, Medium press 1, Heavy press 2\n");
            int.TryParse(Console.ReadLine(), out weight);

            Parcel temp = new Parcel
            {
                Weight = (WeightCategory)weight,
                Priorities = (Priority)priorities
            };

            bl.NewParcel(temp, senderID, receiveID);
        }

        //-----------------------------------------------------------------------------------------------------------//
        // switch 2 - Updates fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AssociateDroneToParcel(IBL.IBL bl)
        {
            int droneId;
            Console.WriteLine("Enter the Id of the drone\n");
            int.TryParse(Console.ReadLine(), out droneId);

            //int parcelId, droneId;
            //List<Parcel> temp = dalObject.GetParcels();
            //foreach (Parcel x in temp) { if (x.DroneId == 0) { Console.WriteLine(x); } }
            //Console.WriteLine("Enter the Id of the parcel");
            //int.TryParse(Console.ReadLine(), out parcelId);

            //List<Drone> temp2 = dalObject.GetDrones();
            //foreach (Drone y in temp2) { if (y.Status == DroneStatuses.Available) { Console.WriteLine(y); } }
            //Console.WriteLine("Enter the Id of the drone");
            //int.TryParse(Console.ReadLine(), out droneId);

            //dalObject.ConnectDroneToParcel(droneId, parcelId);
            bl.connectDroneToParcel(droneId);
        }

        /// <summary>
        /// updates the drone that was assigned to a parcel to pick up the parcel
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void CollectParcelsByDrones(IBL.IBL bl)
        {
            Console.WriteLine("Enter the Id of the drone\n");
            int.TryParse(Console.ReadLine(), out int droneId);

            //int parcelId;
            //List<Parcel> temp = dalObject.GetParcels();
            //foreach (Parcel x in temp) { if (x.DroneId != 0) { Console.WriteLine(x); } }
            //Console.WriteLine("Enter the Id of the parcel");
            //int.TryParse(Console.ReadLine(), out parcelId);

            //dalObject.CollectParcelByDrone(parcelId);
            bl.CollectParcelsByDrone(droneId);
        }

        /// <summary>
        /// updates that the parcel was delivered to the target
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void deliveredParcelToCostumer(IBL.IBL bl)
        {
            Console.WriteLine("Enter the Id of the drone\n");
            int.TryParse(Console.ReadLine(), out int droneId);

            //int parcelId;
            //List<Parcel> temp = dalObject.GetParcels();
            //foreach (Parcel x in temp) { if (x.PickedUp != DateTime.MinValue && x.Delivered == DateTime.MinValue) { Console.WriteLine(x); } }
            //Console.WriteLine("Enter the Id of the parcel");
            //int.TryParse(Console.ReadLine(), out parcelId);

            //dalObject.DeliveredParcel(parcelId);
            bl.deliveredParcel(droneId);
        }

        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void SendDroneToCharge(IBL.IBL bl)
        {
            Console.WriteLine("Enter the Id of the drone\n");
            int.TryParse(Console.ReadLine(), out int droneId);

            //int droneId, stationId;
            //IEnumerable<Drone> temp = dalObject.GetDrones();
            //foreach (Drone y in temp) { if (y.Status != DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            //Console.WriteLine("Enter the Id of the drone to send to charge");
            //int.TryParse(Console.ReadLine(), out droneId);

            //PrintStationWithChargeSolts(dalObject);
            //Console.WriteLine("Enter the Id of the station");
            //int.TryParse(Console.ReadLine(), out stationId);

            //dalObject.SendDroneToBaseCharge(droneId, stationId);
            bl.SendDroneToCharge(droneId);
        }

        /// <summary>
        /// release a drone from charge
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void ReleaseDroneFromChargingBase(IBL.IBL bl)
        {
            Console.WriteLine("Enter the Id of the drone and charge time (in minute)\n");
            int.TryParse(Console.ReadLine(), out int droneId);
            double.TryParse(Console.ReadLine(), out double timeCharge);

            //int droneId;
            //List<Drone> temp = dalObject.GetDrones();
            //foreach (Drone y in temp) { if (y.Status == DroneStatuses.Maintenance) { Console.WriteLine(y); } }
            //Console.WriteLine("Enter the Id of the drone");
            //int.TryParse(Console.ReadLine(), out droneId);

            //dalObject.ReleaseDroneFromCharging(droneId);

            bl.ReleaseDroneFromCharging(droneId, timeCharge);
        }

        private static void updatDrone(IBL.IBL bl)
        {
            Console.WriteLine("Enter the Id of the drone and the new name\n");
            int.TryParse(Console.ReadLine(), out int droneId);
            string model = Console.ReadLine();
            bl.updatDrone(droneId, model);
        }

        private static void updateBase(IBL.IBL bl)
        {
            Console.WriteLine("Enter the number of the base\n");
            int.TryParse(Console.ReadLine(), out int num);
            //isnt finished
        }

        private static void updateCustomer(IBL.IBL bl)
        {
            Console.WriteLine("Enter the number of the base\n");
            int.TryParse(Console.ReadLine(), out int id);
            //isnt finished
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
