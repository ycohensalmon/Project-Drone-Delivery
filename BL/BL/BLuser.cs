using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal partial class BL : IBL
    {
        public bool checkUser(int userId, int password)
        {
            BO.User check = GetUserById(userId);
            if (check.SafePassword != password)
                throw new IncorectInputException("password");
            return check.IsAdmin;
        }

    }
}
