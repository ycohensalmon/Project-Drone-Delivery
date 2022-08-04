using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool checkUser(int userId, int password)
        {
            BO.Customer check = GetCustomerById(userId);
            if (int.Parse(check.SafePassword) != password)
                throw new IncorectInputException("password");
            return check.IsAdmin;
        }
    }
}
