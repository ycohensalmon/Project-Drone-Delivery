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
        IBL.IBL Bl;
        public DronesListWindow(IBL.IBL bl)
        {
            this.Bl = bl; 
            InitializeComponent();
            this.StatusSelector1.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            this.StatusSelector2.ItemsSource = Enum.GetValues(typeof(IBL.BO.Priority));
            DronesListView.ItemsSource = bl.GetDroness();
        }
    }
}
