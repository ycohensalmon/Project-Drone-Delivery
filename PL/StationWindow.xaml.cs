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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        BlApi.IBL myBl;
        public Station station;

        #region add station
        /// <summary>
        /// add station constractor
        /// </summary>
        /// <param name="myBl">the sigelton from the BL layer</param>
        public StationWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            AddStation.Visibility = Visibility.Visible;
            UpdateStation.Visibility = Visibility.Hidden;
        }
        private void ChargeSlot_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChargeSlot.Text != "")
            {
                // check if there are a lattres
                if (ChargeSlot.Text.Any(x => x < '0') == true || ChargeSlot.Text.Any(x => x > '9') == true)
                {
                    ChargeSlot.Foreground = Brushes.Red;
                    return;
                }

                // if the id is nagative or less than 4 digits
                if (ChargeSlot.Text.Length > 3 || int.Parse(ChargeSlot.Text) < 0)
                    ChargeSlot.Foreground = Brushes.Red;
                else
                    ChargeSlot.Foreground = Brushes.Black;
            }
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
        private void AddStation_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Id.Foreground == Brushes.Red) throw new IncorectInputException("id");
                if (ChargeSlot.Foreground == Brushes.Red) throw new IncorectInputException("Charge Slot");

                int StationID = (Id.Text == "") ? throw new EmptyInputException("id") : int.Parse(Id.Text);
                string name = (StationName.Text == "") ? throw new EmptyInputException("name") : StationName.Text;
                int chargeSlot = (ChargeSlot.Text == "") ? throw new EmptyInputException("Charge Slot") : int.Parse(ChargeSlot.Text);
                double lat = SliderLatitude.Value;      // there are no exception bacause the slider can't be empty
                double longt = SliderLongitude.Value;
                Location loc = new Location { Latitude = lat, Longitude = longt };
                Station station = new() { Id = StationID, Name = name, ChargeSolts = chargeSlot, Location = loc };

                myBl.NewStation(station);

                MessageBox.Show("The drone was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update station
        /// <summary>
        /// uptade station constractor
        /// </summary>
        /// <param name="myBl">the sigelton from the BL layer</param>
        /// <param name="selectedItem">the item that was selected from the list of stations</param>
        public StationWindow(Station station)
        {
            this.myBl = BlApi.BlFactory.GetBl();
            this.station = station;
            InitializeComponent();

            AddStation.Visibility = Visibility.Hidden;
            UpdateStation.Visibility = Visibility.Visible;
            if (station.DroneCharges.Count != 0)
                ShowDroneInCharge.Visibility = Visibility.Visible;
            else
                ShowDroneInCharge.Visibility = Visibility.Hidden;


            RefreshUpdate(myBl);
        }

        private void RefreshUpdate(IBL myBl)
        {
            try
            {
                // update the Drone Window
                station = myBl.GetStationById(station.Id);
                this.StationView.DataContext = station;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            new ShowMapWindow(station.Location.Latitude, station.Location.Longitude).ShowDialog();
        }

        private void ShowDroneInCharge_Click(object sender, RoutedEventArgs e)
        {
            DroneChargeList.Visibility = Visibility.Visible;
            DroneChargeList.ItemsSource = station.DroneCharges;
        }
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void DroneCharge_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var droneInCharge = myBl.GetDroneById(((BO.DroneCharge)DroneChargeList.SelectedItem).DroneId);
            DroneWindow droneWindow = new DroneWindow(droneInCharge);
            droneWindow.bottonUpdate.Visibility = Visibility.Collapsed;
            droneWindow.conectToParcel.Visibility = Visibility.Collapsed;
            droneWindow.UpdateModel.Visibility = Visibility.Collapsed;
            droneWindow.btnPlayStop.Visibility = Visibility.Hidden;
            droneWindow.TextToggleButton.Visibility = Visibility.Hidden;

            droneWindow.ShowDialog();
        }
    }
}