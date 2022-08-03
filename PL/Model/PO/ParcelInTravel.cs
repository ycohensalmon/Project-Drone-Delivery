using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ParcelInTravel : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        
        
        private bool inTravel;
        public bool InTravel { get { return inTravel; } set { inTravel = value; OnPropertyChanged("InTravel"); } } // true = waiting for pickup | false = on transit to location
        
        
        private Priority priorities;
        public Priority Priorities { get { return priorities; } set { priorities = value; OnPropertyChanged("Priorities"); } }
        
        
        private WeightCategory weight;
        public WeightCategory Weight { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        
        
        private CustomerInParcel sender;
        public CustomerInParcel Sender { get { return sender; } set { sender = value; OnPropertyChanged("Sender"); } }
        
        
        private CustomerInParcel target;
        public CustomerInParcel Target { get { return target; } set { target = value; OnPropertyChanged("Target"); } }
        
        
        private Location source;
        public Location Source { get { return source; } set { source = value; OnPropertyChanged("Source"); } }
        
        
        private Location destination;
        public Location Destination { get { return destination; } set { destination = value; OnPropertyChanged("Destination"); } }
        
        
        private double distance;
        public double Distance { get { return distance; } set { distance = value; OnPropertyChanged("Distance"); } }



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
