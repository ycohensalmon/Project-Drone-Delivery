using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DroneInList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }


        private string image;
        public string Image { get { return image; } set { image = value; OnPropertyChanged("image"); } }
        

        private string model;
        public string Model { get { return model; } set { model = value; OnPropertyChanged("model"); } }
        
        
        private WeightCategory maxWeight;
        public WeightCategory MaxWeight { get { return maxWeight; } set { maxWeight = value; OnPropertyChanged("maxWeight"); } }
        
        
        private double battery;
        public double Battery { get { return battery; } set { battery = value; OnPropertyChanged("battery"); } }
        
        
        private DroneStatuses status;
        public DroneStatuses Status { get { return status; } set { status = value; OnPropertyChanged("status"); } }
        
        
        private Location location;
        public Location CurrentLocation { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        
        
        private int? numParcel;
        public int? NumParcel { get { return numParcel; } set { numParcel = value; OnPropertyChanged(nameof(NumParcel)); } }


        private bool isDeleted;
        public bool IsDeleted { get { return isDeleted; } set { isDeleted = value; OnPropertyChanged("isDeleted"); } }


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
