using IBL.BO;
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
    /// Logique d'interaction pour DronesListWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL.IBL myBl;
        public DronesListWindow(IBL.IBL bl)
        {
            this.myBl = bl;
            InitializeComponent();
            this.ComboStatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            this.ComboWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategory));
            this.DronesListView.ItemsSource = bl.GetDrones();
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
            new DroneWindow(myBl, this).ShowDialog();
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DroneView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow(myBl, DronesListView.SelectedItem, DronesListView).ShowDialog();
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