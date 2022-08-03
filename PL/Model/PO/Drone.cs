using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    internal class Drone : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        
        
        private string model;
        public string Model { get { return model; } set { model = value; OnPropertyChanged(nameof(Model)); } }
        
        
        private WeightCategory weight;
        public WeightCategory Weight { get { return weight; } set { weight = value; OnPropertyChanged(nameof(Weight)); } }
        
        
        private double battery;
        public double Battery { get { return battery; } set { battery = value; OnPropertyChanged(nameof(Battery)); } }
        
        
        private DroneStatuses status;
        public DroneStatuses Status { get { return status; } set { status = value; OnPropertyChanged(nameof(Status)); } }


        private ParcelInTravel parcelInTravel;
        public ParcelInTravel ParcelInTravel { get { return parcelInTravel; } set { parcelInTravel = value; OnPropertyChanged(nameof(ParcelInTravel)); } }
        
        
        private Location location;
        public Location Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }


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
