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
        public DronesListWindow(IBL bl)
        {
            this.myBl = bl;
            InitializeComponent();
            this.ComboStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses));
            this.ComboWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            var droneList = bl.GetDrones();
            this.DronesListView.ItemsSource = droneList;
        }

        private void ComboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboStatusSelector.SelectedItem != null)
            {
                DroneStatuses status = (DroneStatuses)ComboStatusSelector.SelectedItem;
                if (ComboWeightSelector.SelectedItem == null)
                    this.DronesListView.ItemsSource = myBl.GetDrones().Where(x => x.Status == status);
                else
                {
                    WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;
                    this.DronesListView.ItemsSource = myBl.GetDrones().Where(x => x.Status == status && x.MaxWeight == Weight);
                }
            }
            else
                this.DronesListView.ItemsSource = myBl.GetDrones();
        }

        private void ComboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboWeightSelector.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;

                if (ComboStatusSelector.SelectedItem == null)
                    this.DronesListView.ItemsSource = myBl.GetDrones().Where(x => x.MaxWeight == Weight);
                else
                {
                    DroneStatuses status = (DroneStatuses)ComboStatusSelector.SelectedItem;
                    this.DronesListView.ItemsSource = myBl.GetDrones().Where(x => x.Status == status && x.MaxWeight == Weight);
                }
            }
            else
                this.DronesListView.ItemsSource = myBl.GetDrones();
        }

        private void BottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(myBl).ShowDialog();

            ButtonClearStatus_Click(sender, e);
            ButtonClearWeight_Click(sender, e);
            this.DronesListView.ItemsSource = myBl.GetDrones();
        }


        private void DroneView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonClearStatus_Click(sender, e);
            ButtonClearWeight_Click(sender, e);

            var drone = myBl.GetDroneById(((DroneInList)DronesListView.SelectedItem).Id);
            DroneWindow droneWindow = new(myBl, drone);
            droneWindow.Show();
            droneWindow.bottonUpdate.Click += UpdateDroneList;
            droneWindow.conectToParcel.Click += UpdateDroneList;
            droneWindow.UpdateModel.Click += UpdateDroneList;

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
    }
}