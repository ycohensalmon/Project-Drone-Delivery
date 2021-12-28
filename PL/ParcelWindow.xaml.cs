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
        object selectedItem;

        #region add parcel
        public ParcelWindow(BlApi.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
            UpdateParcel.Visibility = Visibility.Hidden;
            AddParcel.Visibility = Visibility.Visible;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            Priority.ItemsSource = Enum.GetValues(typeof(BO.Priority));

            var NamesCustomer = myBl.GetNamesOfCustomer();
            SenderName.ItemsSource = NamesCustomer;
            ReceiverName.ItemsSource = NamesCustomer;
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
                MessageBox.Show("The drone was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region update parcel
        public ParcelWindow(BlApi.IBL myBl, object selectedItem)
        {
            this.myBl = myBl;
            this.selectedItem = selectedItem;
            this.parcel = myBl.GetParcelById(((BO.ParcelInList)selectedItem).Id);
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
            new DroneWindow(myBl, drone);
        }

        private void TargetInformation_Click(object sender, RoutedEventArgs e)
        {
            var customer = myBl.GetCustomerById(parcel.Sender.Id);
            new CustomerWindow(myBl, customer).Show();
        }

        private void SenderInformation_Click(object sender, RoutedEventArgs e)
        {
            var customer = myBl.GetCustomerById(parcel.Target.Id);
            new CustomerWindow(myBl, customer).Show();
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
