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
    /// Interaction logic for StationsListWindow.xaml
    /// </summary>
    public partial class StationsListWindow : Window
    {
        BlApi.IBL myBl;
        public StationsListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            StasionsListView.ItemsSource = myBl.GetStations();
        }

        private void StationView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var station = myBl.GetStationById(((BO.StationList)StasionsListView.SelectedItem).Id);
            new StationWindow(station).Show();
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow().ShowDialog();
            StasionsListView.ItemsSource = myBl.GetStations();
        }
    }
}
