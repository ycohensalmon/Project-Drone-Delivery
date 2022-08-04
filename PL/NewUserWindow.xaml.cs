using BlApi;
using BO;
using Microsoft.Win32;
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
    /// Logique d'interaction pour NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        private IBL myBl;
        OpenFileDialog op = new OpenFileDialog(); //for getting image input from user
        public NewUserWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string s = op.FileName;
                if (s.Contains("UserIcons"))
                {
                    s = s.Remove(0, s.IndexOf("UserIcons"));
                }
                imageUser.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void AddUser_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Id.Foreground == Brushes.Red) throw new IncorectInputException("id");
                int userId = (Id.Text == "") ? throw new EmptyInputException("id") : int.Parse(Id.Text);
                int phone = (Phone.Text == "") ? throw new EmptyInputException("phone") : int.Parse(Phone.Text);
                string password = (Password.Text == "") ? throw new EmptyInputException("password") : Utils.GetHashPassword(Password.Text);
                string rpassword = (RechakPassword.Text == "") ? throw new EmptyInputException("recheak password") : Utils.GetHashPassword(RechakPassword.Text);
                string name = (UserName.Text == "") ? throw new EmptyInputException("model") : UserName.Text;
                double lat = SliderLatitude.Value;      // there are no exception bacause the slider can't be empty
                double longt = SliderLongitude.Value;
                bool admin = (IsAdmin.IsChecked == true) ? true : false;
                Random rand = new Random();
                op.FileName = (op.FileName == "") ? @"images\user.png" : op.FileName;

                cheakPassword(password, rpassword);
                Location location = new() { Latitude = lat, Longitude = longt };
                Customer customer = new() { Id = userId, Name = name, Phone = phone, Location = location, IsAdmin = admin, Photo = op.FileName, SafePassword = password};

                myBl.NewCostumer(customer);
                Close();
                MessageBox.Show("The user was added successfully", "success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cheakPassword(string password, string rpassword)
        {
            if (password != rpassword)
                throw new IncorectInputPasswordException(password,rpassword);
        }
    }
}
