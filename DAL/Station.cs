using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {

        public struct Station
        {
            private int id;
            private string name;
            private double longitude;
            private double lattitude;
            private int chargeSolts;

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public double Longitude { get => longitude; set => longitude = value; }
            public double Lattitude { get => lattitude; set => lattitude = value; }
            public int ChargeSolts { get => chargeSolts; set => chargeSolts = value; }
            public override string ToString()
            {
                return string.Format("the id is: {0}\nthe name is: {1}\nthe longitude is: {2}\n" +
                    "the lattitude is: {3}\nthe number of available argument positions: {4}\n"
                    ,Id,Name,Longitude,Lattitude,ChargeSolts);
            }
        }
    }
    
}
