using BlApi;
using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for ShowMapWindow.xaml
    /// </summary>
    public partial class ShowMapWindow : Window
    {
        private IBL myBl;
        public ShowMapWindow(double latitude, double longitude)
        {
            InitializeComponent();
            this.myBl = BlApi.BlFactory.GetBl();

            Pushpin pin;
            pin = new();
            pin.Location = new(latitude, longitude);
            myMap.Children.Add(pin);

            //foreach (var item in myBl.GetAllDroneLocations())
            //{
            //    pin = new();
            //    pin.Location = new(item.Longitude, item.Latitude);
            //    myMap.Children.Add(pin);
            //}
            //foreach (var item in myBl.GetAllStationsLocations())
            //{
            //    pin = new();
            //    pin.Location = new(item.Longitude, item.Latitude);
            //    myMap.Children.Add(pin);
            //}
        }
    }
}
