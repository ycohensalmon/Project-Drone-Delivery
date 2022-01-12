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
    /// Interaction logic for ParcelAtCustomerWindow.xaml
    /// </summary>
    public partial class ParcelAtCustomerWindow : Window
    {
        BlApi.IBL myBl;
        public ParcelAtCustomerWindow(List<BO.ParcelAtCustomer> from, string v)
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            TitleParcel.Content = $"Parcel(s) {v} customer";
            parcelAtCustomersView.ItemsSource = from;
        }

        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
