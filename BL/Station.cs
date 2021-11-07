using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Station
        {
            private int id;
            private string name;
            private string longitude;
            private string lattitude;
            private int chargeSolts;

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public string Longitude { get => longitude; set => longitude = value; }
            public string Lattitude { get => lattitude; set => lattitude = value; }
            public int ChargeSolts { get => chargeSolts; set => chargeSolts = value; }
            public override string ToString()
            {
                return string.Format("Id: {0}\nName: {1}\nLongitude: {2}\nLattitude: {3}\nAvailable charging positions: {4}\n"
                    , Id, Name, Longitude, Lattitude, ChargeSolts);
            }
        }
    }
}
