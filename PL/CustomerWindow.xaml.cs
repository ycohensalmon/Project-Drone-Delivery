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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL myBl;
        BO.Customer customer;

        #region update customer
        public CustomerWindow(BlApi.IBL myBl, object selectedItem)
        {
            this.myBl = myBl;
            customer = myBl.GetCustomerById(((BO.CustumerInList)selectedItem).Id);
            InitializeComponent();
            AddCustomer.Visibility = Visibility.Hidden;
            UpdateCustomer.Visibility = Visibility.Visible;
            StationView.DataContext = customer;
        }

        private void ShowParcelsFromCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowParcelsToCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowMap_Click(object sender, RoutedEventArgs e)
        {
            new ShowMapWindow(customer.Location.Latitude, customer.Location.Longitude).ShowDialog();
        }
        #endregion

        #region click
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        #endregion

        #region add customer
        public CustomerWindow(BlApi.IBL myBl)
        {
            this.myBl = myBl;
            InitializeComponent();
            UpdateCustomer.Visibility = Visibility.Hidden;
            AddCustomer.Visibility = Visibility.Visible;
        }

        private void AddStation_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Id.Foreground == Brushes.Red) throw new BO.IncorectInputException("id");
                if (Phone.Foreground == Brushes.Red) throw new BO.IncorectInputException("phone");


                int ID = (Id.Text == "") ? throw new BO.EmptyInputException("id") : int.Parse(Id.Text);
                string name = (Name.Text == "") ? throw new BO.EmptyInputException("name") : Name.Text;
                int phone = (Phone.Text == "") ? throw new BO.EmptyInputException("Phone") : int.Parse(Phone.Text);
                double lat = SliderLatitude.Value;      // there are no exception bacause the slider can't be empty
                double longt = SliderLongitude.Value;
                BO.Location loc = new BO.Location { Latitude = lat, Longitude = longt };
                BO.Customer customer = new() { Id = ID, Name = name, Phone = phone, Location = loc };

                myBl.NewCostumer(customer);

                MessageBox.Show("The customer was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Id.Text != "")
            {
                // check if there are a lattres
                if (Id.Text.Any(x => x < '0') == true || Id.Text.Any(x => x > '9') == true)
                {
                    Id.Foreground = Brushes.Red;
                    return;
                }

                // if the id is nagative or less than 4 digits
                if (Id.Text.Length < 4 || int.Parse(Id.Text) < 0)
                    Id.Foreground = Brushes.Red;
                else
                    Id.Foreground = Brushes.Black;
            }
        }

        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Phone.Text != "")
            {
                // check if there are a lattres
                if (Phone.Text.Any(x => x < '0') == true || Phone.Text.Any(x => x > '9') == true)
                {
                    Phone.Foreground = Brushes.Red;
                    return;
                }

                // if the id is nagative or less than 10 digits
                if (Phone.Text.Length < 10 || int.Parse(Phone.Text) < 0)
                    Phone.Foreground = Brushes.Red;
                else
                    Phone.Foreground = Brushes.Black;
            }
        }
        #endregion
    }
}
