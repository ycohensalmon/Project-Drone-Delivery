using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategory { Light, Medium, Heavy }
        public enum DroneStatuses { Available, maintenance, delivery }
        public enum Priority { Normal, fast, emergency }
        public enum DateTime { Creation, association, collection, delivery }
        public enum ModelDrones { Cobra, Magic, Mavic_Air, DJI, Mickcara}

    }
}
