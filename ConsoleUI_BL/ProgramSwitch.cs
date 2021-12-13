using BL.BO;
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
        // Switchs //
        //-----------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// options of switch1 
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch1(BL.IBL bl)
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
                    throw new WrongEnumValuesException("Menu", 1, 4);
            }
        }

        /// <summary>
        /// options of switch2
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch2(BL.IBL bl)
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    UpdateDrone(bl);
                    break;
                case 2:
                    UpdateBase(bl);
                    break;
                case 3:
                    UpdateCustomer(bl);
                    break;
                case 4:
                    AssociateDroneToParcel(bl);
                    break;
                case 5:
                    CollectParcelsByDrones(bl);
                    break;
                case 6:
                    DeliveredParcelToCostumer(bl);
                    break;
                case 7:
                    SendDroneToCharge(bl);
                    break;
                case 8:
                    ReleaseDroneFromChargingBase(bl);
                    break;
                default:
                    throw new WrongEnumValuesException("Menu", 1, 8);
            }
        }

        /// <summary>
        /// options of switch3
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch3(BL.IBL bl)
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
                    throw new WrongEnumValuesException("Menu", 1, 4);
            }
        }

        /// <summary>
        /// options of switch4
        /// </summary>
        /// <param name="dalObject">the parameter that include all the lists</param>
        private static void Switch4(BL.IBL bl)
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
                    throw new WrongEnumValuesException("Menu", 1, 6);
            }
        }
    }
}
