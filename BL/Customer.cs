using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            private Int32 id;
            private string name;
            private Int32 phone;
            private string longitude;
            private string lattitude;

            public Int32 Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public Int32 Phone { get => phone; set => phone = value; }
            public string Longitude { get => longitude; set => longitude = value; }
            public string Latittude { get => lattitude; set => lattitude = value; }
            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, Longitude:{Longitude}, Latittude:{Latittude}";
            }
        }
    }
}
