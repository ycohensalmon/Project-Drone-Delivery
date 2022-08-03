using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DroneCharge : INotifyPropertyChanged
    {
        private int droneId;
        public int DroneId { get { return droneId; } set { droneId = value; OnPropertyChanged("DroneId"); } }


        private int stationId;
        public int StationId { get { return stationId; } set { stationId = value; OnPropertyChanged("StationId"); } }
        
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
