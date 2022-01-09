using BlApi;
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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        BlApi.IBL myBl;
        public CustomerListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            var customerList = myBl.GetCustomers();
            this.customersView.ItemsSource = customerList;
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow().ShowDialog();
            customersView.ItemsSource = myBl.GetCustomers();
        }

        private void Customer_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var customer = myBl.GetCustomerById(((BO.CustumerInList)customersView.SelectedItem).Id);
            new CustomerWindow(customer).Show();

        }
    }
}
