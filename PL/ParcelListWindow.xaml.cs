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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        BlApi.IBL myBl;

        public ParcelListWindow(BlApi.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();

            parcelsView.ItemsSource = myBl.GetParcels();
        }

        public ParcelListWindow(IBL myBl, List<ParcelAtCustomer> parcels, string v)
        {
            this.myBl = myBl;
            InitializeComponent();
            customerLable.Content = $"Parcel(s) {v} customer";
            parcelsView.ItemsSource = parcels;
            parcelsView.IsEnabled = true;
        }

        private void parcelsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ParcelWindow(myBl, parcelsView.SelectedItem).Show();
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(myBl).ShowDialog();
        }
    }
}
