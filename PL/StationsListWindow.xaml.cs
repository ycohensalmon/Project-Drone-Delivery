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

        public StationsListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();

            // Drone tab
            droneList = myBl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
            this.DroneComboStatus.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            this.DroneComboWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));

            // station tab
            stationList = myBl.GetStations();
            StasionsListView.ItemsSource = stationList;

            // Parcel tab
            pacelsList = myBl.GetParcels();
            parcelsView.ItemsSource = pacelsList;
            this.ParcelComboWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            this.ParcelComboStatus.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatuses));
        }


        #region drone

        #region drone grouping
        private void DroneWeightGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("MaxWeight");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void DroneStatusGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones();
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
                    this.droneList = myBl.GetDrones().Where(x => x.Status == status);
                    this.DronesListView.ItemsSource = droneList;
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)DroneComboWeight.SelectedItem;
                    this.droneList = myBl.GetDrones().Where(x => x.Status == status && x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;
                }
            }
            else
            {
                this.droneList = myBl.GetDrones();
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
                    droneList = myBl.GetDrones().Where(x => x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;

                }
                else
                {
                    DroneStatuses status = (DroneStatuses)DroneComboStatus.SelectedItem;
                    this.droneList = myBl.GetDrones().Where(x => x.Status == status && x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;
                }
            }
            else
            {
                this.droneList = myBl.GetDrones();
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
            Border dataDrone = e.OriginalSource as Border;

            var drone = myBl.GetDroneById(((DroneInList)dataDrone.DataContext).Id);
            DroneWindow droneWindow = new(drone);

            droneWindow.bottonUpdate.Click += UpdateDroneList;
            droneWindow.conectToParcel.Click += UpdateDroneList;
            droneWindow.UpdateModel.Click += UpdateDroneList;
            droneWindow.ShowDialog();
        }

        private void UpdateDroneList(object s, EventArgs e)
        {
            DronesListView.ItemsSource = myBl.GetDrones();
        }

        private void BottonAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().ShowDialog();

            ButtonDroneClearStatus_Click(sender, e);
            ButtonDroneClearWeight_Click(sender, e);
            this.DronesListView.ItemsSource = myBl.GetDrones();
        }
        #endregion
        #endregion


        #region station

        #region station click
        private void StationView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Border datatation = e.OriginalSource as Border;

            var station = myBl.GetStationById(((BO.StationList)datatation.DataContext).Id);
            StationWindow stationWindow = new(station);

            stationWindow.ShowDialog();
            //var station = myBl.GetStationById(((BO.StationList)StasionsListView.SelectedItem).Id);
            //new StationWindow(station).Show();
        }
        private void bottonAddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow().ShowDialog();
            StasionsListView.ItemsSource = myBl.GetStations();
        }
        #endregion

        #region station grouping
        private void StationChargeGroupe_Click(object sender, RoutedEventArgs e)
        {
            var StationList = myBl.GetStations();
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
        }
        private void ParcelView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonParcelClearWeight_Click(sender, e);
            ButtonParcelClearWeight_Click(sender, e);

            Border dataParcel = e.OriginalSource as Border;
            var parcel = myBl.GetParcelById(((ParcelInList)dataParcel.DataContext).Id);
            ParcelWindow parcelWindow = new(parcel);

            parcelWindow.ShowDialog();
        }
        #endregion

        #region parcel groiping
        private void GroupingSender_Click(object sender, RoutedEventArgs e)
        {
            pacelsList = myBl.GetParcels();
            parcelsView.ItemsSource = pacelsList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsView.ItemsSource);
            PropertyGroupDescription groupDescription = new("SenderName");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void GroupingTarget_Click(object sender, RoutedEventArgs e)
        {
            pacelsList = myBl.GetParcels();
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
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status);
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)ParcelComboWeight.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels();
            }
        }
        private void ParcelComboWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParcelComboWeight.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)ParcelComboWeight.SelectedItem;

                if (ParcelComboStatus.SelectedItem == null)
                {
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Weight == Weight);

                }
                else
                {
                    ParcelStatuses status = (ParcelStatuses)ParcelComboStatus.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels();
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


        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
