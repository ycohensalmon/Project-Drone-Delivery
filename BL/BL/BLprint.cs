using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public DroneInList GetDroneById(int droneId)
            {
                DroneInList drone = drones.Find(x => x.Id == droneId);
                if (drone.Id != droneId)
                    throw new ItemNotFoundException(droneId, "DroneInList");
                return drone;
            }

            public Station GetStationById(int stationId)
            {
                IDAL.DO.Station station = dalObj.GetStationById(stationId);

                Location temp = new Location { Latitude = station.Latitude, Longitude = station.Longitude };
               
                List<IDAL.DO.DroneCharge> droneChargeIDAL = dalObj.GetDroneCharges().ToList();
                List<DroneCharge> droneChargesBL = new();
                DroneCharge droneCharge = new();

                //fill the List droneChargesBL
                foreach (var item in droneChargeIDAL)
                {
                    if (item.StationId == station.Id)
                    {
                        droneCharge.DroneId = item.DroneId;
                        droneCharge.StationId = item.StationId;
                        droneChargesBL.Add(droneCharge);
                    }
                }

                Station printStstion = new Station
                {
                    Id = station.Id,
                    Name = station.Name,
                    Location = temp,
                    ChargeSolts = station.ChargeSolts,
                    DroneCharges = droneChargesBL
                };

                return printStstion;
            }

            public void PrintStations() { dalObj.GetStations(); }
            public void PrintCustomers() { dalObj.GetCustomers(); }
            public void PrintParcels() { dalObj.GetParcels(); }
            public void PrintDrones() { dalObj.GetDrones(); }
        }
    }
}
