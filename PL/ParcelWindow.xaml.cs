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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        BlApi.IBL myBl;
        BO.Parcel parcel;

        #region add parcel
        public ParcelWindow(string name = "")
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            UpdateParcel.Visibility = Visibility.Hidden;
            AddParcel.Visibility = Visibility.Visible;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            Priority.ItemsSource = Enum.GetValues(typeof(BO.Priority));

            var NamesCustomer = myBl.GetNamesOfCustomer();
            SenderName.ItemsSource = NamesCustomer;
            ReceiverName.ItemsSource = NamesCustomer;

            if(name != "")
            {
                SenderName.SelectedItem = name;
                SenderName.IsEnabled = false;
            }
        }

        private void AddParcel_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int senderID = (SenderName.SelectedItem == null) ? throw new BO.EmptyInputException("sender Id") : myBl.GetCustomerIdByName((string)SenderName.SelectedItem);
                int targetID = (ReceiverName.SelectedItem == null) ? throw new BO.EmptyInputException("target Id") : myBl.GetCustomerIdByName((string)ReceiverName.SelectedItem);
                BO.WeightCategory weight = (Weight.SelectedItem == null) ? throw new BO.EmptyInputException("weight") : (BO.WeightCategory)Weight.SelectedItem;
                BO.Priority priority = (Priority.SelectedItem == null) ? throw new BO.EmptyInputException("station") : (BO.Priority)Priority.SelectedItem;

                BO.Parcel parcel = new() { Weight = weight, Priorities = priority };

                myBl.NewParcel(parcel, senderID, targetID);
                MessageBox.Show("The parcel was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update parcel
        public ParcelWindow(BO.Parcel parcel)
        {
            this.myBl = BlApi.BlFactory.GetBl();
            this.parcel = parcel;
            InitializeComponent();
            AddParcel.Visibility = Visibility.Hidden;
            UpdateParcel.Visibility = Visibility.Visible;
            parcelInformation.DataContext = parcel;
            if (parcel.Scheduled != null && parcel.Delivered == null)
            {
                droneInformation.DataContext = parcel;
                droneInformation.Visibility = Visibility.Visible;
            }
            else
                droneInformation.Visibility = Visibility.Hidden;
        }

        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            new ShowMapWindow(parcel.Drone.Location.Latitude, parcel.Drone.Location.Longitude).ShowDialog();
        }
        private void DroneInformation_Click(object sender, RoutedEventArgs e)
        {
            var drone = myBl.GetDroneById(parcel.Drone.Id);
            new DroneWindow(drone);
        }

        private void TargetInformation_Click(object sender, RoutedEventArgs e)
        {
            var customer = myBl.GetCustomerById(parcel.Sender.Id);
            new CustomerWindow(customer).Show();
        }

        private void SenderInformation_Click(object sender, RoutedEventArgs e)
        {
            var customer = myBl.GetCustomerById(parcel.Target.Id);
            new CustomerWindow(customer).Show();
        }
        #endregion

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
