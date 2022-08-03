using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Parcel : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        
        
        private CustomerInParcel sender;
        public CustomerInParcel Sender { get { return sender; } set { sender = value; OnPropertyChanged("Sender"); } }
        
        
        private CustomerInParcel target;
        public CustomerInParcel Target { get { return target; } set { target = value; OnPropertyChanged("Target"); } }


        private DroneInParcel drone;
        public DroneInParcel Drone { get { return drone; } set { drone = value; OnPropertyChanged("Drone"); } }


        private WeightCategory weight;
        public WeightCategory Weight { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        
        
        private Priority priorities;
        public Priority Priorities { get { return priorities; } set { priorities = value; OnPropertyChanged("Priorities"); } }
        
        
        private DateTime? requested = null;
        public DateTime? Requested { get { return requested; } set { requested = value; OnPropertyChanged("Requested"); } }
        
        
        private DateTime? scheduled = null;
        public DateTime? Scheduled { get { return scheduled; } set { scheduled = value; OnPropertyChanged("Scheduled"); } }
        
        
        private DateTime? pickedUp = null;
        public DateTime? PickedUp { get { return pickedUp; } set { pickedUp = value; OnPropertyChanged("PickedUp"); } }
        
        
        private DateTime? delivered = null;
        public DateTime? Delivered { get { return delivered; } set { delivered = value; OnPropertyChanged("Delivered"); } }


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
