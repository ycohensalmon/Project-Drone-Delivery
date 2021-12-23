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

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL myBl;
        private Drone drone;
        private Parcel parcel;
        ListView droneList;
        public event EventHandler DroneChanged;

        #region add drone
        public DroneWindow(IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
            UpdateDrone.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Visible;
            this.MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            List<string> NameStations = new();
            foreach (var item in myBl.GetStations(x => x.ChargeSoltsAvailable != 0)) NameStations.Add(item.Name);
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
        public DroneWindow(IBL myBl, object selectedItem, ListView droneList)
        {
            this.myBl = myBl;
            this.drone = myBl.GetDroneById(((DroneInList)selectedItem).Id);
            this.droneList = droneList;
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            UpdateDrone.Visibility = Visibility.Visible;

            //DroneChanged += refreshWindow;
            //DroneChanged(this, EventArgs.Empty);
            RefreshButtonUpdate(myBl);
        }
        private void RefreshButtonUpdate(IBL myBl)
        {
            DroneStatuses status = drone.Status;

            switch (status)
            {
                case DroneStatuses.Available:
                    bottonUpdate.Content = "Send to charge";
                    conectToParcel.Visibility = Visibility.Visible;
                    break;
                case DroneStatuses.Maintenance:
                    bottonUpdate.Content = "Release from charge";
                    conectToParcel.Visibility = Visibility.Hidden;
                    break;
                case DroneStatuses.Delivery:
                    parcel = myBl.GetParcelById(drone.ParcelInTravel.Id);
                    if (parcel.Scheduled != null && parcel.PickedUp == null)
                    {
                        bottonUpdate.Content = "Collect delivery";
                    }
                    else if (parcel.PickedUp != null && parcel.Delivered == null)
                    {
                        bottonUpdate.Content = "Delivered parcel by this drone";
                    }
                    else
                        bottonUpdate.Content = "error";
                    break;
                default:
                    break;
            }

            try
            {
                // update the Drone Window
                this.DroneView.Content = myBl.GetDroneById(drone.Id).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void refreshWindow(object s, EventArgs e)
        {
            DroneView.Content = myBl.GetDroneById(drone.Id);
        }
        #endregion

        #region buttons update
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
                //DroneChanged += refreshWindow;
                //drone = myBl.GetDroneById(drone.Id);
                //droneList.DataContext = myBl.GetDrones();
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
                //DroneChanged += refreshWindow;
                //drone = myBl.GetDroneById(drone.Id);
                myBl.ConnectDroneToParcel(drone.Id);
                //droneList.DataContext = myBl.GetDrones();
                MessageBox.Show("The drone was update successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                conectToParcel.Visibility = Visibility.Hidden;
                RefreshButtonUpdate(myBl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void updateName_Click(object sender, RoutedEventArgs e)
        {

            // Add this label to form

            // Creating and setting the properties of TextBox1
            TextBox Mytextbox = new TextBox();
            Mytextbox.Name = "ModelUpdate";

        }
        #endregion

        #region somes buttons
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
        #endregion
    }
}