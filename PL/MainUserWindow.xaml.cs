using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour MainUserWindow.xaml
    /// </summary>
    public partial class MainUserWindow : Window
    {
        private IBL myBl;
        private CustumerInList customers;
        Customer customer;
        public MainUserWindow(CustumerInList customers)
        {
            InitializeComponent();
            this.myBl = BlApi.BlFactory.GetBl();
            this.customers = customers;
            customer = myBl.GetCustomerById(customers.Id);
            this.DataContext = customer;
            

            if (customer.ParcelsFromCustomer.Count != 0)
                ParcelsFromCustomer.Visibility = Visibility.Visible;
            else
                ParcelsFromCustomer.Visibility = Visibility.Collapsed;

            if (customer.ParcelsToCustomer.Count != 0)
                ParcelsToCustomer.Visibility = Visibility.Visible;
            else
                ParcelsToCustomer.Visibility = Visibility.Collapsed;

        }

        private void ShowParcelsFromCustomer_Click(object sender, RoutedEventArgs e)
        {
            var from = customer.ParcelsFromCustomer;
            new ParcelAtCustomerWindow(from, "from").Show();
        }

        private void ShowParcelsToCustomer_Click(object sender, RoutedEventArgs e)
        {
            var to = customer.ParcelsToCustomer;
            new ParcelAtCustomerWindow(to, "to").Show();
        }

        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            new ShowMapWindow(customer.Location.Latitude, customer.Location.Longitude).ShowDialog();
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendNewParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(customer.Name).Show();
        }
    }
}
