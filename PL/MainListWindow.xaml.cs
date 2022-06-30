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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationsListWindow.xaml
    /// </summary>
    public partial class StationsListWindow : Window
    {
        BlApi.IBL myBl;
        IEnumerable<DroneInList> droneList;
        IEnumerable<StationList> stationList;
        IEnumerable<ParcelInList> pacelsList;
        IEnumerable<CustumerInList> customerList;

        public StationsListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();

            // Drone tab
            droneList = myBl.GetDrones(x => x.IsDeleted == false);
            this.DronesListView.ItemsSource = droneList;
            this.DroneComboStatus.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            this.DroneComboWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));

            // station tab
            stationList = myBl.GetStations(x => x.IsDeleted == false);
            StasionsListView.ItemsSource = stationList;

            // Parcel tab
            pacelsList = myBl.GetParcels(x => x.IsDeleted == false);
            parcelsView.ItemsSource = pacelsList;
            this.ParcelComboWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            this.ParcelComboStatus.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatuses));

            // Customer tab
            customerList = myBl.GetCustomers(x => x.IsDeleted == false);
            this.customersView.ItemsSource = customerList;
        }


        #region drone

        #region drone grouping
        private void DroneWeightGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones(x => x.IsDeleted == false);
            this.DronesListView.ItemsSource = droneList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("MaxWeight");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void DroneStatusGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones(x => x.IsDeleted == false);
            this.DronesListView.ItemsSource = droneList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("Status");
            view.GroupDescriptions.Add(groupDescription);
        }
        #endregion

        #region drone filtering

        private void DroneComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneComboStatus.SelectedItem != null)
            {
                DroneStatuses status = (DroneStatuses)DroneComboStatus.SelectedItem;
                if (DroneComboWeight.SelectedItem == null)
                {
                    this.droneList = myBl.GetDrones(x => x.IsDeleted == false).Where(x => x.Status == status);
                    this.DronesListView.ItemsSource = droneList;
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)DroneComboWeight.SelectedItem;
                    this.droneList = myBl.GetDrones(x => x.IsDeleted == false).Where(x => x.Status == status && x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;
                }
            }
            else
            {
                this.droneList = myBl.GetDrones(x => x.IsDeleted == false);
                this.DronesListView.ItemsSource = droneList;
            }
        }
        private void DroneComboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneComboWeight.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)DroneComboWeight.SelectedItem;

                if (DroneComboStatus.SelectedItem == null)
                {
                    droneList = myBl.GetDrones(x => x.IsDeleted == false).Where(x => x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;

                }
                else
                {
                    DroneStatuses status = (DroneStatuses)DroneComboStatus.SelectedItem;
                    this.droneList = myBl.GetDrones(x => x.IsDeleted == false).Where(x => x.Status == status && x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;
                }
            }
            else
            {
                this.droneList = myBl.GetDrones(x => x.IsDeleted == false);
                this.DronesListView.ItemsSource = droneList;
            }
        }
        private void ButtonDroneClearStatus_Click(object sender, RoutedEventArgs e)
        {
            this.DroneComboStatus.SelectedItem = null;
        }
        private void ButtonDroneClearWeight_Click(object sender, RoutedEventArgs e)
        {
            this.DroneComboWeight.SelectedItem = null;
        }

        #endregion

        #region drone click
        private void DroneView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonDroneClearStatus_Click(sender, e);
            ButtonDroneClearWeight_Click(sender, e);

            int id = 0;
            if (e.OriginalSource is Border)
            {
                Border dataDrone = e.OriginalSource as Border;
                id = ((DroneInList)dataDrone.DataContext).Id;
            }
            if (e.OriginalSource is StackPanel)
            {
                StackPanel dataDrone = e.OriginalSource as StackPanel;
                id = ((DroneInList)dataDrone.DataContext).Id;
            }

            NewDroneWindow(id);
        }

        private void NewDroneWindow(int id)
        {
            var drone = myBl.GetDroneById(id);
            DroneWindow droneWindow = new(drone);

            droneWindow.bottonUpdate.Click += UpdateDroneList;
            droneWindow.conectToParcel.Click += UpdateDroneList;
            droneWindow.UpdateModel.Click += UpdateDroneList;
            droneWindow.UpdateList.TextChanged += UpdateDroneList;
            droneWindow.Show();
        }

        private void DroneEdit_Click(object sender, RoutedEventArgs e)
        {
            Button drone = sender as Button;
            int id = ((DroneInList)drone.DataContext).Id;

            NewDroneWindow(id);
        }

        private void UpdateDroneList(object s, EventArgs e)
        {
            DronesListView.ItemsSource = myBl.GetDrones(x => x.IsDeleted == false);
            customersView.ItemsSource = myBl.GetCustomers(x => x.IsDeleted == false);
            parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false);
            StasionsListView.ItemsSource = myBl.GetStations(x => x.IsDeleted == false);
        }

        private void BottonAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().ShowDialog();

            ButtonDroneClearStatus_Click(sender, e);
            ButtonDroneClearWeight_Click(sender, e);
            this.DronesListView.ItemsSource = myBl.GetDrones(x => x.IsDeleted == false);
        }

        private void DroneDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this drone ??", "Warning"
                                , MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Button drone = sender as Button;
                    int id = ((DroneInList)drone.DataContext).Id;
                    myBl.DeleteDrone(id);
                    DronesListView.ItemsSource = myBl.GetDrones(x => x.IsDeleted == false);
                }
            }
            catch (Exception ex)
            {
                DronesListView.ItemsSource = "";
                MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion
        #endregion


        #region station

        #region station click
        private void StationView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = 0;
            if (e.OriginalSource is Border)
            {
                Border dataDrone = e.OriginalSource as Border;
                id = ((StationList)dataDrone.DataContext).Id;
            }
            if (e.OriginalSource is StackPanel)
            {
                StackPanel dataDrone = e.OriginalSource as StackPanel;
                id = ((StationList)dataDrone.DataContext).Id;
            }

            NewStationWindow(id);
        }

        private void StationEdit_Click(object sender, RoutedEventArgs e)
        {
            Button station = sender as Button;
            int id = ((StationList)station.DataContext).Id;

            NewStationWindow(id);
        }

        private void NewStationWindow(int id)
        {
            var station = myBl.GetStationById(id);
            StationWindow stationWindow = new(station);

            stationWindow.ShowDialog();
        }

        private void bottonAddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow().ShowDialog();
            StasionsListView.ItemsSource = myBl.GetStations(x => x.IsDeleted == false);
        }

        private void StationDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this station ??", "Warning"
                                , MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Button station = sender as Button;
                    int id = ((StationList)station.DataContext).Id;
                    myBl.DeleteStation(id);
                    StasionsListView.ItemsSource = myBl.GetStations(x=>x.IsDeleted == false);
                }
            }
            catch (Exception ex)
            {
                StasionsListView.ItemsSource = "";
                MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region station grouping
        private void StationChargeGroupe_Click(object sender, RoutedEventArgs e)
        {
            var StationList = myBl.GetStations(x => x.IsDeleted == false);
            this.StasionsListView.ItemsSource = StationList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StasionsListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("ChargeSoltsAvailable");
            view.GroupDescriptions.Add(groupDescription);
        }
        #endregion
        #endregion


        #region parcel

        #region parcel click
        private void bottonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow().ShowDialog();
            parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false);
        }
        private void ParcelView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonParcelClearWeight_Click(sender, e);
            ButtonParcelClearWeight_Click(sender, e);

            int id = -1;
            if (e.OriginalSource is Border)
            {
                Border dataParcel = e.OriginalSource as Border;
                id = ((ParcelInList)dataParcel.DataContext).Id;
            }
            if (e.OriginalSource is StackPanel)
            {
                StackPanel dataParcel = e.OriginalSource as StackPanel;
                id = ((ParcelInList)dataParcel.DataContext).Id;
            }

            NewParcelWindow(id);
        }
        private void ParcelEdit_Click(object sender, RoutedEventArgs e)
        {
            Button parcel = sender as Button;
            int id = ((ParcelInList)parcel.DataContext).Id;

            NewParcelWindow(id);
        }
        private void NewParcelWindow(int id)
        {
            var parcel = myBl.GetParcelById(id);
            ParcelWindow parcelWindow = new(parcel);

            parcelWindow.ShowDialog();
        }

        private void DeleteParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this parcel ??", "Warning"
                                , MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Button parcel = sender as Button;
                    int id = ((ParcelInList)parcel.DataContext).Id;
                    myBl.DeleteParcel(id);
                    parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false);
                }
            }
            catch (Exception ex)
            {
                parcelsView.ItemsSource = "";
                MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region parcel groiping
        private void GroupingSender_Click(object sender, RoutedEventArgs e)
        {
            pacelsList = myBl.GetParcels(x => x.IsDeleted == false);
            parcelsView.ItemsSource = pacelsList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsView.ItemsSource);
            PropertyGroupDescription groupDescription = new("SenderName");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void GroupingTarget_Click(object sender, RoutedEventArgs e)
        {
            pacelsList = myBl.GetParcels(x => x.IsDeleted == false);
            parcelsView.ItemsSource = pacelsList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsView.ItemsSource);
            PropertyGroupDescription groupDescription = new("TargetName");
            view.GroupDescriptions.Add(groupDescription);
        }
        #endregion

        #region parcel filtred
        private void ParcelComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParcelComboStatus.SelectedItem != null)
            {
                ParcelStatuses status = (ParcelStatuses)ParcelComboStatus.SelectedItem;
                if (ParcelComboWeight.SelectedItem == null)
                {
                    this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false).Where(x => x.Status == status);
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)ParcelComboWeight.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false).Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false);
            }
        }
        private void ParcelComboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParcelComboWeight.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)ParcelComboWeight.SelectedItem;

                if (ParcelComboStatus.SelectedItem == null)
                {
                    this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false).Where(x => x.Weight == Weight);

                }
                else
                {
                    ParcelStatuses status = (ParcelStatuses)ParcelComboStatus.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false).Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels(x => x.IsDeleted == false);
            }
        }
        private void ButtonParcelClearStatus_Click(object sender, RoutedEventArgs e)
        {
            this.ParcelComboStatus.SelectedItem = null;
        }
        private void ButtonParcelClearWeight_Click(object sender, RoutedEventArgs e)
        {
            this.ParcelComboWeight.SelectedItem = null;
        }
        #endregion
        #endregion

        #region Customer

        #region customer click
        private void CustomerBottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow().ShowDialog();
            customersView.ItemsSource = myBl.GetCustomers(x => x.IsDeleted == false);
        }

        private void Customer_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var customer = myBl.GetCustomerById(((BO.CustumerInList)customersView.SelectedItem).Id);

            NewCustomerWindow(customer);
        }

        private static void NewCustomerWindow(Customer customer)
        {
            CustomerWindow customerWindow = new(customer);
            customerWindow.ShowDialog();
        }

        private void CustomerEdit_Click(object sender, RoutedEventArgs e)
        {
            Button customer = sender as Button;
            var cust = myBl.GetCustomerById(((CustumerInList)customer.DataContext).Id);

            NewCustomerWindow(cust);
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure that you want to delete this customer ??", "Warning"
                                , MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Button customer = sender as Button;
                    int id = ((CustumerInList)customer.DataContext).Id;
                    myBl.DeleteCustomer(id);
                    customersView.ItemsSource = myBl.GetCustomers(x => x.IsDeleted == false);
                }
            }
            catch (Exception ex)
            {
                customersView.ItemsSource = "";
                MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion
        #endregion

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
