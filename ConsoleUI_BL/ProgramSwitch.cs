using BO;
using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

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
                    throw new WrongEnumValuesException("Menu", 1, 4);
            }
        }

        /// <summary>
        /// options of switch2
        /// </summary>
        private static void Switch2()
        {
            int choises;
            int.TryParse(Console.ReadLine(), out choises);
            switch (choises)
            {
                case 1:
                    UpdateDrone();
                    break;
                case 2:
                    UpdateBase();
                    break;
                case 3:
                    UpdateCustomer();
                    break;
                case 4:
                    AssociateDroneToParcel();
                    break;
                case 5:
                    CollectParcelsByDrones();
                    break;
                case 6:
                    DeliveredParcelToCostumer();
                    break;
                case 7:
                    SendDroneToCharge();
                    break;
                case 8:
                    ReleaseDroneFromChargingBase();
                    break;
                default:
                    throw new WrongEnumValuesException("Menu", 1, 8);
            }
        }

        /// <summary>
        /// options of switch3
        /// </summary>
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
                    throw new WrongEnumValuesException("Menu", 1, 4);
            }
        }

        /// <summary>
        /// options of switch4
        /// </summary>
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
                    throw new WrongEnumValuesException("Menu", 1, 6);
            }
        }
    }
}
