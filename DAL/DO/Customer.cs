using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Customer
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public Int32 Phone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name:{Name}, Phone:{Phone},Latittude:{Utils.Sexagesimal(Latitude, 'N')}," +
                                                        $"Longitude:{Utils.Sexagesimal(Longitude, 'E')}";
        }
    }
}
