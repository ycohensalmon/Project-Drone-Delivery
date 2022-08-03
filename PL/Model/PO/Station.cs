using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Station : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        
        
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        
        private Location location;
        public Location Location { get { return location; } set { location = value; OnPropertyChanged("Location"); } }

        
        private int chargeSolts;
        public int ChargeSolts { get { return chargeSolts; } set { chargeSolts = value; OnPropertyChanged("ChargeSolts"); } }

        
        private List<DroneCharge> droneCharges;
        public List<DroneCharge> DroneCharges { get { return droneCharges; } set { droneCharges = value; OnPropertyChanged("DroneCharges"); } }
        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
