using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChargeSoltsAvailable { get; set; }
        public int ChargeSoltsBusy { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            return string.Format($"Id: {Id}\nName: {Name}\nchargeSoltsAvailable: {ChargeSoltsAvailable}\nchargeSoltsBusy: {ChargeSoltsBusy}");
        }
    }
}
