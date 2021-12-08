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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL.IBL myBl;
        private DroneInList drone;
 
        public DroneWindow(IBL.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
            this.Title = "Add drone";
            UpdateGrid.Visibility = Visibility.Hidden;
            AddGrid.Visibility = Visibility.Visible;
            this.maxWeight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategory));
            this.status.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));


        }

        public DroneWindow(IBL.IBL myBl, object selectedItem, ListView dronesListView)
        {
            this.myBl = myBl;
            this.drone = (DroneInList)selectedItem;
            InitializeComponent();
            AddGrid.Visibility = Visibility.Hidden;
            UpdateGrid.Visibility = Visibility.Visible;
            this.Title = "Update drone";
            this.DroneView.Content = myBl.GetDroneById(drone.Id);


        }

        private void maxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //myBl.NewDroneInList
        }

        private void DroneView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
