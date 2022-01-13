using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int ChargeSolts { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}\nName: {1}\nLattitude: {2}\nLongitude: {3}\nAvailable charging positions: {4}\n"
                , Id, Name, Utils.Sexagesimal(Latitude, 'N'), Utils.Sexagesimal(Longitude, 'E'), ChargeSolts);
        }
    }
}


