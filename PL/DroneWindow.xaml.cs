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
    /// Logique d'interaction pour DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL.IBL myBl;

        public DroneWindow(IBL.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
        }

        public DroneWindow(IBL.IBL myBl, IBL.BO.DroneInList drone)
        {
            this.myBl = myBl;
            InitializeComponent();
            DroneView.ItemsSource = (System.Collections.IEnumerable)myBl.GetDrones().First(x=> x.Id == drone.Id);
        }
    }
}
