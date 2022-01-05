using System;
using System.Runtime.Serialization;
using BL;
using BlApi;
using BO;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static IBL bl = BlFactory.GetBl();

        static void Main(string[] args)
        {
            PrintEnterToTheProject();

            int choises = 0;

            Pause();

            do
            {
                if (choises == 1)
                {
                    PrintLoginMenu();
                    int.TryParse(Console.ReadLine(), out choises);
                    Console.WriteLine("add Id: (9 digits)");
                    int id = AddId9(true, true);
                    try
                    {
                        switch (choises)
                        {
                            case 1:
                                if (CheckLoginAsManagerOrUser(id))
                                    continue;
                                UserInterfacePrinting();
                                SwitchUser(id);
                                break;
                            case 2:
                                //printCreateUserMenu();
                                break;
                            case 3:
                                break;
                            default:
                                throw new WrongEnumValuesException("Menu", 1, 3);
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintException(ex);
                    }
                }
            } while (choises != 2);
           
            choises = 0;

            do
            {

                PrintFirstMenu();
                int.TryParse(Console.ReadLine(), out choises);

                try
                {
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
                        case 5:
                            break;
                        default:
                            throw new WrongEnumValuesException("Menu", 1, 5);
                    }
                }
                catch (Exception ex)
                {
                    PrintException(ex);
                }
            } while (choises != 5);
        }
    }
}
