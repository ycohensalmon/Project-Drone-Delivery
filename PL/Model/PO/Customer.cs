using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Customer
    {
        private int? id = null;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged("id"); } }
        
        
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("name"); } }
        
        
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("phone"); } }
        
        
        private Location location;
        public Location Location { get { return location; } set { location = value; OnPropertyChanged("location"); } }
        
        
        private List<ParcelAtCustomer> parcelsFromCustomer;
        public List<ParcelAtCustomer> ParcelsFromCustomer { get { return parcelsFromCustomer; } set { parcelsFromCustomer = value; OnPropertyChanged("ParcelsFromCustomer"); } } // change to list
        
        
        private List<ParcelAtCustomer> parcelsToCustomer;
        public List<ParcelAtCustomer> ParcelsToCustomer { get { return parcelsToCustomer; } set { parcelsToCustomer = value; OnPropertyChanged("ParcelsToCustomer"); } }// change to list


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
