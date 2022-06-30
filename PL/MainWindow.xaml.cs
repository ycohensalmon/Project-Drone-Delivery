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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal readonly IBL myBl;

        public MainWindow()
        {
            myBl = BlFactory.GetBl();
            InitializeComponent();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            myBl.ClearDroneCharge();
            Close();
        }

        private void StationsClick(object sender, MouseButtonEventArgs e)
        {
            new LoginWindow().ShowDialog();
        }

        private void wathsappClick(object sender, RoutedEventArgs e)
        {
            openBrowser("https://web.whatsapp.com/");
        }

        private void safariClick(object sender, RoutedEventArgs e)
        {
            openBrowser("https://www.google.co.il/?hl=iw");
        }

        private void phoneClick(object sender, RoutedEventArgs e)
        {
            openBrowser("https://www.apple.com/il/iphone/");
        }
        private void cameraClick(object sender, RoutedEventArgs e)
        {
            openBrowser("https://webcamera.io/");
        }
        private void meteoClick(object sender, MouseButtonEventArgs e)
        {
            openBrowser("http://www.meteo-tech.co.il/%D7%AA%D7%97%D7%96%D7%99%D7%AA/");
        }

        private static void openBrowser(string url)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "chrome";
            process.StartInfo.Arguments = url;
            process.Start();
        }
    }
}
