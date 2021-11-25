using IBL.BO;
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
        // switch 1 - Adds fonctions //
        //-----------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// add stetion to the list 
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists </param>
        private static void AddStation(IBL.IBL bl)
        {
            int id = AddId4(false);
            string name = AddName();
            double latitude = Addlatitude();
            double longitude = AddLongitude();
            int chargeSlots = AddChargeSlot();

            Location loc = new Location
            {
                Latitude = latitude,
                Longitude = longitude
            };

            Station temp = new Station
            {
                Id = id,
                Name = name,
                Location = loc,
                ChargeSolts = chargeSlots,
                DroneCharges = null
            };
            bl.NewStation(temp);
        }

        /// <summary>
        /// add drone to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddDrone(IBL.IBL bl)
        {
            int id = AddId4(false);
            string model = AddModel(false);
            int maxWeight = AddMaxWeight();
            int stationId = AddNumStation();

            DroneInList temp = new DroneInList
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategory)maxWeight - 1,
            };

            bl.NewDroneInList(temp, stationId);
        }

        /// <summary>
        /// add Customer to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddCustomer(IBL.IBL bl)
        {
            int id = AddId9(false, false);
            string name = AddName();
            int phone = AddPhone();
            double latitude = Addlatitude();
            double longitude = AddLongitude();

            Location loc = new Location
            {
                Latitude = latitude,
                Longitude = longitude
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
        /// add parcel to the list
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void AddParcel(IBL.IBL bl)
        {
            int senderID = AddId9(true, false);
            int receiveID = AddId9(false, true);
            int priorities = AddPriorities();
            int weight = AddWeigth();

            Parcel temp = new Parcel
            {
                Weight = (WeightCategory)weight,
                Priorities = (Priority)priorities
            };

            bl.NewParcel(temp, senderID, receiveID);
        }

        private static int AddChargeSlot()
        {
            int chargeSlots;
            do
            {
                Console.WriteLine("add chargeSolts:\n");
                if (int.TryParse(Console.ReadLine(), out chargeSlots) == false)
                    throw new OnlyDigitsException("ChargeSlots");
                if (chargeSlots < 0)
                    throw new NegetiveValueException("Charge Slots");
            } while (chargeSlots < 0);

            return chargeSlots;
        }
        private static string AddName()
        {
            Console.WriteLine("add name:\n");
            string name = Console.ReadLine();
            return name;
        }
        private static double Addlatitude()
        {
            double latitude;
            do
            {
                Console.WriteLine("add lattitude: (example 12.123456)\n");
                if (double.TryParse(Console.ReadLine(), out latitude) == false)
                    throw new OnlyDigitsException("Lattitude");
                if (latitude < 0)
                    throw new NegetiveValueException("Lattitude", 8, 12.123456);

            } while (latitude < 0);
            return latitude;
        }
        private static double AddLongitude()
        {
            double longitude;
            do
            {
                Console.WriteLine("add longitude: (example 12.123456)\n");
                if (double.TryParse(Console.ReadLine(), out longitude) == false)
                    throw new OnlyDigitsException("Longitude");
                if (longitude < 0)
                    throw new NegetiveValueException("Longitude", 8, 12.123456);

            } while (longitude < 0);
            return longitude;
        }
        private static int AddId4(bool drone)
        {
            int id;
            do
            {
                if (drone == false)
                    Console.WriteLine("add Id: (4 digits)\n");
                if (drone == true)
                    Console.WriteLine("Enter the Id of the drone\n");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new OnlyDigitsException("ID");
                if (id < 0)
                    throw new NegetiveValueException("ID", 4);

            } while (id < 0);
            return id;
        }
        private static int AddId9(bool sender, bool target)
        {
            int id;
            do
            {
                if (sender == true && target == false)
                    Console.WriteLine("add Id of the sending customer: (9 digits)");
                if (sender == false && target == true)
                    Console.WriteLine("add Id of the receiving customer: (9 digits)\n");
                if (sender == false && target == false)
                    Console.WriteLine("add Id: (9 digits)\n");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new OnlyDigitsException("ID");
                if (id < 0)
                    throw new NegetiveValueException("ID", 9);

            } while (id < 0);
            return id;
        }
        private static int AddPhone()
        {
            int phone;
            do
            {
                Console.WriteLine("add Phone:\n");
                if (int.TryParse(Console.ReadLine(), out phone) == false)
                    throw new OnlyDigitsException("Phone");
                if (phone < 0)
                    throw new NegetiveValueException("Phone", 10, 0501234567);

            } while (phone < 0);
            return phone;
        }
        private static int AddMaxWeight()
        {
            int maxWeight;
            do
            {
                Console.WriteLine("add maximum weight that the drone can carry (1,2 or 3 KG)\n");
                if (int.TryParse(Console.ReadLine(), out maxWeight) == false)
                    throw new OnlyDigitsException("Max Weight");
                if (maxWeight < 0)
                    throw new NegetiveValueException("Max Weight");
            } while (maxWeight < 0);

            return maxWeight;
        }
        private static int AddNumStation()
        {
            int stationID;
            do
            {
                Console.WriteLine("Select a station number for initial charging\n");
                if (int.TryParse(Console.ReadLine(), out stationID) == false)
                    throw new OnlyDigitsException("Num Station");
                if (stationID < 0)
                    throw new NegetiveValueException("Num Station");
            } while (stationID < 0);
            return stationID;
        }
        private static int AddPriorities()
        {
            int priorities;
            do
            {
                Console.WriteLine("choise the priority, for Normal press 0, Fast press 1, Emergency press 2\n");
                if (int.TryParse(Console.ReadLine(), out priorities) == false)
                    throw new OnlyDigitsException("Max Weight");
                if (priorities < 0)
                    throw new NegetiveValueException("Max Weight");
            } while (priorities < 0);

            return priorities;
        }
        private static string AddModel(bool newModel)
        {
            if (newModel == false)
                Console.WriteLine("add model:\n");
            if(newModel == true)
                Console.WriteLine("add new model:\n");
            string model = Console.ReadLine();
            return model;
        }
        private static int AddWeigth()
        {
            int weight;

            do
            {
                Console.WriteLine("chose the weight of the parcel, for Light press 0, Medium press 1, Heavy press 2\n");
                if (int.TryParse(Console.ReadLine(), out weight) == false)
                    throw new OnlyDigitsException("Weight");
                if (weight < 0)
                    throw new NegetiveValueException("Weight");
            } while (weight < 0);

            return weight;
        }
    }
}
