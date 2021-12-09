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
        DronesListWindow droneslistWindow;


        public DroneWindow(IBL.IBL myBl, DronesListWindow listWindow)
        {
            droneslistWindow = listWindow;
            this.myBl = myBl;
            InitializeComponent();
            this.Title = "Add drone";
            UpdateGrid.Visibility = Visibility.Hidden;
            AddGrid.Visibility = Visibility.Visible;
            this.maxWeight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategory));
            this.station.ItemsSource = myBl.GetStations();
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

            DroneStatuses status = drone.Status;

            switch (status)
            {
                case DroneStatuses.Available:
                    bottonUpdate.Content = "Release from charge";
                    break;
                case DroneStatuses.Maintenance:
                    bottonUpdate.Content = "Send to charge";
                    break;
                case DroneStatuses.Delivery:
                    bottonUpdate.Content = "Collect delivery";
                    break;
                default:
                    break;
            }
        }

        private void maxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int droneID = int.Parse(id.Text);
            WeightCategory  MaxWeight = (WeightCategory)maxWeight.SelectedItem;
            string Model = model.Text;
            StationList Station = (StationList)station.SelectedItem;

            int StationId = Station.Id;
            DroneInList drone = new() {Id = droneID, Model = Model, MaxWeight = MaxWeight};

            try
            {
                myBl.NewDroneInList(drone, StationId);
                droneslistWindow.Close();
                DronesListWindow listWindow = new DronesListWindow(myBl);
                listWindow.ComboStatusSelector.SelectedItem = droneslistWindow.ComboStatusSelector.SelectedItem;
                listWindow.ComboWeightSelector.SelectedItem = droneslistWindow.ComboWeightSelector.SelectedItem;
                listWindow.Show();
                Close();

                MessageBox.Show("The drone was added successfully", "success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void stationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void bottonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drone.Status == DroneStatuses.Maintenance)
                    myBl.ReleaseDroneFromCharging(drone.Id);
                    if (drone.Status == DroneStatuses.Available)
                        myBl.SendDroneToCharge(drone.Id);
                if (drone.Status == DroneStatuses.Delivery)
                    myBl.CollectParcelsByDrone(drone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
