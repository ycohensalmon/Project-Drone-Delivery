using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            private int id;
            private string name;
            private string phone;
            private double longitude;
            private double lattitude;

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public string Phone { get => phone; set => phone = value; }
            public double Longitude { get => longitude; set => longitude = value; }
            public double Latittude { get => lattitude; set => lattitude = value; }
            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone}, Longitude:{Longitude}, Latittude:{Latittude}";
            }
        }
    }
}
