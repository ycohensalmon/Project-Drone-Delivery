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
        private DroneInList drone;
        DO.Parcel parcel;
        ListView droneList;

        // add drone
        public DroneWindow(IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
            UpdateDrone.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Visible;
            this.MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            List<string> NameStations = new();
            foreach (var item in myBl.GetStations()) NameStations.Add(item.Name);
            this.Station.ItemsSource = NameStations;
        }

        private void maxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaxWeight.SelectedItem == null)
                MaxWeight.Foreground = Brushes.Red;
            else
                MaxWeight.Foreground = Brushes.Black;

        }

        private void UIElement_OnMouseLeave(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int droneID = (Id.Text == "") ? throw new EmptyInputException("id") : int.Parse(Id.Text);
                string model =  (Model.Text == "") ? throw new EmptyInputException("model") : Model.Text;
                WeightCategory weight = (MaxWeight.SelectedItem == null) ? throw new EmptyInputException("weight") : (WeightCategory)MaxWeight.SelectedItem;


                string nameStation = (Station.SelectedItem == null) ? throw new EmptyInputException("station") : (string)Station.SelectedItem;                 
                StationList tempStation = myBl.GetStations().FirstOrDefault(x => x.Name == nameStation);
                 
                int StationId = tempStation.Id;
                DroneInList drone = new() { Id = droneID, Model = model, MaxWeight = weight };


                myBl.NewDroneInList(drone, StationId);
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

        public DroneWindow(IBL myBl, object selectedItem, ListView droneList)
        {
            this.myBl = myBl;
            this.drone = (DroneInList)selectedItem;
            this.droneList = droneList;
            InitializeComponent();
            AddDrone.Visibility = Visibility.Hidden;
            UpdateDrone.Visibility = Visibility.Visible;

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
                    break;
                case DroneStatuses.Delivery:

                    parcel = myBl.GetParcelWasConnectToParcel(drone.Id, out drone);
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
                this.DroneView.Content = myBl.GetDroneById(drone.Id).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
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

                MessageBox.Show("success");
                RefreshButtonUpdate(myBl);
                droneList = (ListView)myBl.GetDrones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }

        }
        private void conectToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.ConnectDroneToParcel(drone.Id);
                MessageBox.Show("success");
                conectToParcel.Visibility = Visibility.Hidden;
                RefreshButtonUpdate(myBl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
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
            if (Id.Text == "")
                Id.Foreground = Brushes.Red;
            else
                Id.Foreground = Brushes.Black;
        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Id_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (Model.Text == "")
                Model = WrongText;
        }

        private void MaxWeight_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void MaxWeight_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void MaxWeight_PreviewDragEnter(object sender, DragEventArgs e)
        {

        }

        private void MaxWeight_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void updateName_Click(object sender, RoutedEventArgs e)
        {

            // Add this label to form

            // Creating and setting the properties of TextBox1
            TextBox Mytextbox = new TextBox();
            Mytextbox.Name = "ModelUpdate";

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