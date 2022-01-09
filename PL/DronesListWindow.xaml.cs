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
using BlApi;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DronesListWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL myBl;
        IEnumerable<DroneInList> droneList;
        public DronesListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            this.ComboStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            this.ComboWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            droneList = myBl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
        }

        public DronesListWindow(IBL bl, List<DroneCharge> droneCharges)
        {
            this.DronesListView.ItemsSource = droneCharges;
        }

        private void ComboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboStatusSelector.SelectedItem != null)
            {
                DroneStatuses status = (DroneStatuses)ComboStatusSelector.SelectedItem;
                if (ComboWeightSelector.SelectedItem == null)
                {
                    this.droneList = myBl.GetDrones().Where(x => x.Status == status);
                    this.DronesListView.ItemsSource = droneList;
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;
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

        private void ComboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboWeightSelector.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;

                if (ComboStatusSelector.SelectedItem == null)
                {
                    droneList = myBl.GetDrones().Where(x => x.MaxWeight == Weight);
                    this.DronesListView.ItemsSource = droneList;

                }
                else
                {
                    DroneStatuses status = (DroneStatuses)ComboStatusSelector.SelectedItem;
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

        private void BottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().ShowDialog();

            ButtonClearStatus_Click(sender, e);
            ButtonClearWeight_Click(sender, e);
            this.DronesListView.ItemsSource = myBl.GetDrones();
        }


        private void DroneView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonClearStatus_Click(sender, e);
            ButtonClearWeight_Click(sender, e);
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

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonClearStatus_Click(object sender, RoutedEventArgs e)
        {
            this.ComboStatusSelector.SelectedItem = null;
        }

        private void ButtonClearWeight_Click(object sender, RoutedEventArgs e)
        {
            this.ComboWeightSelector.SelectedItem = null;
        }


        private void WeightGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("MaxWeight");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void StatusGroupe_Click(object sender, RoutedEventArgs e)
        {
            var droneList = myBl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new("Status");
            view.GroupDescriptions.Add(groupDescription);
        }
    }
}