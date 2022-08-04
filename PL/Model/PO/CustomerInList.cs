using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class CustomerInList : INotifyPropertyChanged
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        
        
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("name"); } }
        
        
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("phone"); } }

        
        private int parcelsShippedAndDelivered;
        public int ParcelsShippedAndDelivered { get { return parcelsShippedAndDelivered; } set { parcelsShippedAndDelivered = value; OnPropertyChanged("ParcelsShippedAndDelivered"); } }


        private int parcelsShippedAndNotDelivered;
        public int ParcelsShippedAndNotDelivered { get { return parcelsShippedAndNotDelivered; } set { parcelsShippedAndNotDelivered = value; OnPropertyChanged("ParcelsShippedAndNotDelivered"); } }


        private int parcelsHeRecieved;
        public int ParcelsHeRecieved { get { return parcelsHeRecieved; } set { parcelsHeRecieved = value; OnPropertyChanged("ParcelsHeRecieved"); } }


        private int parcelsOnTheWay;
        public int ParcelsOnTheWay { get { return parcelsOnTheWay; } set { parcelsOnTheWay = value; OnPropertyChanged("ParcelsOnTheWay"); } }


        private bool isDeleted;
        public bool IsDeleted { get { return isDeleted; } set { isDeleted = value; OnPropertyChanged("IsDeleted"); } }


        private string safePassword;
        public string SafePassword { get { return safePassword; } set { safePassword = value; OnPropertyChanged("SafePassword"); } }


        private string photo;
        public string Photo { get { return photo; } set { photo = value; OnPropertyChanged("Photo"); } }


        private bool isAdmin;
        public bool IsAdmin { get { return isAdmin; } set { isAdmin = value; OnPropertyChanged("IsAdmin"); } }

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
