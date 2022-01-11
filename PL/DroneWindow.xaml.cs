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
using BO;
using BlApi;
using BL;
using System.ComponentModel;
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL myBl;
        public Drone drone;
        internal BackgroundWorker worker;

        #region add drone
        public DroneWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();

            UpdateDrone.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Visible;
            this.MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            var NameStations = myBl.GetNamesOfAvailableChargeSolts();
            this.Station.ItemsSource = NameStations;
        }
        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Id.Text != "")
            {
                // check if there are a lattres
                if (Id.Text.Any(x => x < '0') == true || Id.Text.Any(x => x > '9') == true)
                {
                    Id.Foreground = Brushes.Red;
                    return;
                }

                // if the id is nagative or less than 4 digits
                if (Id.Text.Length < 4 || int.Parse(Id.Text) < 0)
                    Id.Foreground = Brushes.Red;
                else
                    Id.Foreground = Brushes.Black;
            }
        }
        private void AddDrone_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Id.Foreground == Brushes.Red) throw new IncorectInputException("id");
                int droneID = (Id.Text == "") ? throw new EmptyInputException("id") : int.Parse(Id.Text);
                string model = (Model.Text == "") ? throw new EmptyInputException("model") : Model.Text;
                WeightCategory weight = (MaxWeight.SelectedItem == null) ? throw new EmptyInputException("weight") : (WeightCategory)MaxWeight.SelectedItem;
                string nameStation = (Station.SelectedItem == null) ? throw new EmptyInputException("station") : (string)Station.SelectedItem;
                StationList tempStation = myBl.GetStations().FirstOrDefault(x => x.Name == nameStation);

                int StationId = tempStation.Id;
                DroneInList drone = new() { Id = droneID, Model = model, MaxWeight = weight };


                myBl.NewDroneInList(drone, StationId);
                Close();
                MessageBox.Show("The drone was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update drone refresh
        private void RefreshButtonUpdate(IBL myBl)
        {
            DroneStatuses status = drone.Status;
            //bottonUpdate.DataContext = drone;
            //conectToParcel.DataContext = drone;
            switch (drone.Status)
            {
                case DroneStatuses.Available:
                    bottonUpdate.Content = "Send to charge";
                    conectToParcel.Visibility = Visibility.Visible;
                    ShowParcel.Visibility = Visibility.Hidden;
                    break;
                case DroneStatuses.Maintenance:
                    bottonUpdate.Content = "Release from charge";
                    conectToParcel.Visibility = Visibility.Hidden;
                    break;
                case DroneStatuses.Delivery:
                    ShowParcel.Visibility = Visibility.Visible;
                    if (drone.ParcelInTravel.InTravel == true)
                        bottonUpdate.Content = "Collect delivery";
                    else
                    {
                        bottonUpdate.Content = "Delivered parcel by this drone";
                    }
                    break;
            }

            try
            {
                // update the Drone Window
                drone = myBl.GetDroneById(drone.Id);
                this.DroneView.DataContext = drone;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void refreshWindow(object s, EventArgs e)
        {
            drone = myBl.GetDroneById(drone.Id);
            this.DroneView.DataContext = drone;
        }
        #endregion

        #region update
        public DroneWindow(Drone drone)
        {
            this.myBl = BlApi.BlFactory.GetBl();
            this.drone = drone;
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            UpdateDrone.Visibility = Visibility.Visible;
            DroneView.DataContext = drone;

            RefreshButtonUpdate(myBl);
        }
        private void bottonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (drone.Status)
                {
                    case DroneStatuses.Available:
                        int stationId = myBl.SendDroneToCharge(drone.Id);
                        BO.Station station = myBl.GetStationById(stationId);
                        MessageBox.Show($"The drone was sent for charging at {station.Name}", "success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case DroneStatuses.Maintenance:
                        myBl.ReleaseDroneFromCharging(drone.Id);
                        break;
                    case DroneStatuses.Delivery:
                        if (drone.ParcelInTravel.InTravel != true)
                            myBl.CollectParcelsByDrone(drone.Id);
                        else
                            myBl.DeliveredParcel(drone.Id);
                        break;
                    default:
                        break;
                }
                drone = myBl.GetDroneById(drone.Id);
                MessageBox.Show("The drone was update successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshButtonUpdate(myBl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void conectToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.ConnectDroneToParcel(drone.Id);
                drone = myBl.GetDroneById(drone.Id);

                MessageBox.Show("The drone was update successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                conectToParcel.Visibility = Visibility.Hidden;
                ShowParcel.Visibility = Visibility.Visible;
                RefreshButtonUpdate(myBl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region click
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// if we want to move to window
        /// </summary>
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            new ShowMapWindow(drone.Location.Latitude, drone.Location.Longitude).ShowDialog();
        }

        private void ShowParcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateModel_Click(object sender, RoutedEventArgs e)
        {
            if (modelTextBox.IsEnabled == false)
            {
                UpdateModelIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Verified;
            }
            else
            {
                myBl.UpdateDrone(drone.Id, modelTextBox.Text);
                drone = myBl.GetDroneById(drone.Id);
                modelTextBox.DataContext = drone;
                UpdateModelIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Update;
            }
        }
        #endregion


        private void btnPlayStop_Checked(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += autoMode_DoWork;
            worker.ProgressChanged += autoMode_ProgressChanged;
            worker.RunWorkerCompleted += autoMode_RunWorkerCompleted;

            //hiding the action buttons
            //btnReleaseCharge.Visibility = Visibility.Hidden;
            //btnCharge.Visibility = Visibility.Hidden;
            //btnDroneToDelivery.Visibility = Visibility.Hidden;
            //btnDronePickUp.Visibility = Visibility.Hidden;
            //btnDroneDeliver.Visibility = Visibility.Hidden;
            //modelToPrint.IsEnabled = false;
            worker.RunWorkerAsync();

        }

        private void btnPlayStop_Unchecked(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();

            //modelToPrint.IsEnabled = true;
            //btnReleaseCharge.Visibility = Visibility.Visible;
            //btnCharge.Visibility = Visibility.Visible;
            //btnDroneToDelivery.Visibility = Visibility.Visible;
            //btnDronePickUp.Visibility = Visibility.Visible;
            //btnDroneDeliver.Visibility = Visibility.Visible;
            //display();
        }

        private void autoMode_DoWork(object sender, DoWorkEventArgs e)
        {
            myBl.ActivSimulator(drone.Id, update, stop);
        }

        private void autoMode_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            drone = myBl.GetDroneById(drone.Id); //getting the updated drone from the bl
            DroneView.DataContext = drone;

            //if (drone.InShipping.Id == 0)
            //{
            //    parcelExpander.IsExpanded = false;
            //    parcelExpander.IsEnabled = false;
            //}
            //else
            //{
            //    parcelExpander.IsExpanded = true;
            //    parcelExpander.IsEnabled = true;
            //}

            //if (droneList != null)
            //    droneList.checkFilters(); //update according to filters
        }

        private void autoMode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        private void update()
        {
            worker.ReportProgress(0);
        }

        private bool stop()
        {
            return worker.CancellationPending;
        }
    }
}