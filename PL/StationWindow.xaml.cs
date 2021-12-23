using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        BlApi.IBL myBl;
        private object selectedItem;

        public StationWindow(BlApi.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
        }

        public StationWindow(IBL myBl, object selectedItem) : this(myBl)
        {
            this.selectedItem = selectedItem;
            InitializeComponent();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChargeSlot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ChargeSlot_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Id.Text == "")
                Id.Foreground = Brushes.Red;
            else
                Id.Foreground = Brushes.Black;
        }

        private void Id_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void AddStation_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int StationID = (Id.Text == "") ? throw new EmptyInputException("id") : int.Parse(Id.Text);
                string name = (StationName.Text == "") ? throw new EmptyInputException("name") : StationName.Text;
                int chargeSlot = (ChargeSlot.Text == "") ? throw new EmptyInputException("Charge Slot") : int.Parse(ChargeSlot.Text);
                double lat = SliderLatitude.Value;      // there are no exception bacause the slider can't be empty
                double longt = SliderLongitude.Value;
                Location loc = new Location { Latitude = lat, Longitude = longt };
                Station station = new() { Id = StationID, Name = name, ChargeSolts = chargeSlot, Location = loc };

                myBl.NewStation(station);

                MessageBox.Show("The drone was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
