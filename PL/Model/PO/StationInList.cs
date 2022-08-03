using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class StationInList : INotifyPropertyChanged
    {
        private int? id;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        
        
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        
        
        private int chargeSoltsAvailable;
        public int ChargeSoltsAvailable { get { return chargeSoltsAvailable; } set { chargeSoltsAvailable = value; OnPropertyChanged("ChargeSoltsAvailable"); } }
        
        
        private int chargeSoltsBusy;
        public int ChargeSoltsBusy { get { return chargeSoltsBusy; } set { chargeSoltsBusy = value; OnPropertyChanged("ChargeSoltsBusy"); } }


        private bool isDeleted;
        public bool IsDeleted { get { return isDeleted; } set { isDeleted = value; OnPropertyChanged("IsDeleted"); } }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //}
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
        #endregion
    }
}
