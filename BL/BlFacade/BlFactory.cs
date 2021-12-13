using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlFacade
{
    public static class BlFactory
    {
        public static IBL GetBL()
        {
            IBL bL = new BL();
            return bL;
        }
    }
}


