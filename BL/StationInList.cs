using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class StationList
        {
            private int id;
            private string name;
            private int chargeSoltsAvailable;
            private int chargeSoltsBusy;

            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public int ChargeSoltsAvailable { get => chargeSoltsAvailable; set => chargeSoltsAvailable = value; }
            public int ChargeSoltsBusy { get => chargeSoltsBusy; set => chargeSoltsBusy = value; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}\nName: {Name}\nchargeSoltsAvailable: {chargeSoltsAvailable}\nchargeSoltsBusy: {chargeSoltsBusy}");
            }
        }
    }
}
