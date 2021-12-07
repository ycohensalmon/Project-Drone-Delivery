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
            this.StatusSelector1.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            this.StatusSelector2.ItemsSource = Enum.GetValues(typeof(IBL.BO.Priority));
            this.DronesListView.ItemsSource = bl.GetDroness();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StatusSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses status = (DroneStatuses)StatusSelector1.SelectedItem;
            this.DronesListView.ItemsSource = myBl.GetDroness().Where(x => x.Status == status);
        }

        private void botton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(myBl).ShowDialog();
        }
    }
}