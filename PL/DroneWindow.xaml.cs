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

        // add drone
        public DroneWindow(IBL.IBL myBl, DronesListWindow listWindow)
        {
            droneslistWindow = listWindow;
            this.myBl = myBl;
            InitializeComponent();
            DroneTextBlock.Text = "Add a Drone";
            UpdateDrone.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Visible;
            this.MaxWeight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategory));
            this.Station.ItemsSource = myBl.GetStations();
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
                Refrash();

                MessageBox.Show("The drone was added successfully", "success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void Refrash()
        {
            droneslistWindow.Close();
            DronesListWindow listWindow = new DronesListWindow(myBl);
            listWindow.ComboStatusSelector.SelectedItem = droneslistWindow.ComboStatusSelector.SelectedItem;
            listWindow.ComboWeightSelector.SelectedItem = droneslistWindow.ComboWeightSelector.SelectedItem;
            listWindow.Show();
            Close();
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
                Refrash();

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

                Refrash();
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            MessageBox.Show("success");

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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // if we want to move to window
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Model.Text == "")
                Model = WrongText;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Id_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (Model.Text == "")
                Model = WrongText;
        }

        /*private static int AddId4(bool drone, bool station)
        {
            int id;
            do
            {
                if (drone == false && station == false)
                    Console.WriteLine("add Id: (4 digits)");
                if (drone == true && station == false)
                    Console.WriteLine("Enter the Id of the drone");
                if (drone == false && station == true)
                    Console.WriteLine("Enter the Id of the station");
                if (int.TryParse(Console.ReadLine(), out id) == false)
                    throw new OnlyDigitsException("ID");
                if (id < 0)
                    throw new NegetiveValueException("ID", 4);

            } while (id < 0);
            return id;

        }*/

    }
}