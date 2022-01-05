using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        private static bool CheckLoginAsManagerOrUser(int id)
        {
            Console.WriteLine("enter your password");
            int.TryParse(Console.ReadLine(), out int password);
            return bl.checkUser(id, password.GetHashCode());
        }
    }
}
