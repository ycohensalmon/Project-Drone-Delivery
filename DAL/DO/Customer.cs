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
            private Int32 id;
            private string name;
            private Int32 phone;
            private double latitude;  // 'N'
            private double longitude; // 'E'

            public Int32 Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public Int32 Phone { get => phone; set => phone = value; }
            public double Latitude { get => latitude; set => latitude = value; }
            public double Longitude { get => longitude; set => longitude = value; }
            public override string ToString()
            {
                return $"Id: {Id}, Name:{Name}, Phone:{Phone},Latittude:{DalObject.DataSource.Sexagesimal(Latitude, 'N')}," +
                                                            $"Longitude:{DalObject.DataSource.Sexagesimal(Longitude, 'E')}";
            }
        }
    }
}
