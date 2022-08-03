using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ParcelInList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        
        
        private string senderName;
        public string SenderName { get { return senderName; } set { senderName = value; OnPropertyChanged("SenderName"); } }
        
        
        private string targetName;
        public string TargetName { get { return targetName; } set { targetName = value; OnPropertyChanged("TargetName"); } }
        
        
        private WeightCategory weight;
        public WeightCategory Weight { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        
        
        private Priority priorities;
        public Priority Priorities { get { return priorities; } set { priorities = value; OnPropertyChanged("Priorities"); } }
        
        
        private ParcelStatuses status;
        public ParcelStatuses Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }


        private bool isDeleted;
        public bool IsDeleted { get { return isDeleted; } set { isDeleted = value; OnPropertyChanged("IsDeleted"); } }


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
