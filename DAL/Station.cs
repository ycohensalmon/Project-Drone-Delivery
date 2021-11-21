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
            private double latitude;  // 'N'
            private double longitude; // 'E'
            private int chargeSolts;

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public double Latitude { get => latitude; set => latitude = value; }
            public double Longitude { get => longitude; set => longitude = value; }
            public int ChargeSolts { get => chargeSolts; set => chargeSolts = value; }
            public override string ToString()
            {
                return string.Format("Id: {0}\nName: {1}\nLattitude: {2}\nLongitude: {3}\nAvailable charging positions: {4}\n"
                    , Id,Name,DalObject.DataSource.Sexagesimal(Latitude, 'N'), DalObject.DataSource.Sexagesimal(Longitude, 'E'),ChargeSolts);
            }
        }
    }
    
}
