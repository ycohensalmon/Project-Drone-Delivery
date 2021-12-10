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
        ListView droneInLists;
        DronesListWindow droneslistWindow;
        IDAL.DO.Parcel parcel;


        public DroneWindow(IBL.IBL myBl, DronesListWindow listWindow)
        {
            droneslistWindow = listWindow;
            this.myBl = myBl;
            InitializeComponent();
            //this.Title = "Add drone";
            DroneTextBlock.Text = "Add a Drone";
            UpdateDrone.Visibility = Visibility.Hidden;
            //AddGrid.Visibility = Visibility.Hidden;
            AdditionDrone.Visibility = Visibility.Visible;
            this.MaxWeight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategory));
            this.Station.ItemsSource = myBl.GetStations();
        }

        public DroneWindow(IBL.IBL myBl, object selectedItem, ListView dronesListView)
        {
            this.myBl = myBl;
            this.drone = (DroneInList)selectedItem;
            this.droneInLists = dronesListView;
            InitializeComponent();
            DroneTextBlock.Text = "Add a Drone";
            AddDrone.Visibility = Visibility.Hidden;
            UpdateDrone.Visibility = Visibility.Visible;
            this.Title = "Update drone";
            try
            {
                this.DroneView.Content = myBl.GetDroneById(drone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }

            DroneStatuses status = drone.Status;

            switch (status)
            {
                case DroneStatuses.Available:
                    bottonUpdate.Content = "Send to charge";
                    conectToParcel.Visibility = Visibility.Visible;
                    break;
                case DroneStatuses.Maintenance:
                    bottonUpdate.Content = "Release from charge";
                    break;
                case DroneStatuses.Delivery:

                    parcel = myBl.GetParcelWasConnectToParcel(drone.Id, out drone);
                    if (parcel.Scheduled != null && parcel.PickedUp == null)
                        bottonUpdate.Content = "Collect delivery";
                    else if (parcel.PickedUp != null && parcel.Delivered == null)
                        bottonUpdate.Content = "Delivered parcel by this drone";
                    else
                        bottonUpdate.Content = "error";
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
            int droneID = int.Parse(Id.Text);
            WeightCategory  Weight = (WeightCategory)MaxWeight.SelectedItem;
            string model = Model.Text;
            StationList station = (StationList)Station.SelectedItem;

            int StationId = station.Id;
            DroneInList drone = new() {Id = droneID, Model = model, MaxWeight = Weight };

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

                switch (drone.Status)
                {
                    case DroneStatuses.Available:
                        myBl.SendDroneToCharge(drone.Id);

                        break;
                    case DroneStatuses.Maintenance:
                        myBl.ReleaseDroneFromCharging(drone.Id);

                        break;
                    case DroneStatuses.Delivery:
                        if (parcel.Scheduled != null && parcel.PickedUp == null)
                            myBl.CollectParcelsByDrone(drone.Id);
                        else if (parcel.PickedUp != null && parcel.Delivered == null)
                            myBl.DeliveredParcel(drone.Id);

                        break;
                    default:
                        break;
                }

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            MessageBox.Show("success");

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void conectToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.ConnectDroneToParcel(drone.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            new DronesListWindow(myBl);
            MessageBox.Show("success");
            Close();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void UIElement_OnMouseLeave(object sender, MouseButtonEventArgs e)
        {
            int droneID = int.Parse(Id.Text);
            WeightCategory weight = (WeightCategory)MaxWeight.SelectedItem;
            string model = Model.Text;
            StationList station = (StationList)Station.SelectedItem;

            int StationId = station.Id;
            DroneInList drone = new() { Id = droneID, Model = model, MaxWeight = weight };

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

        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}