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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        BlApi.IBL myBl;

        public ParcelListWindow()
        {
            this.myBl = BlApi.BlFactory.GetBl();
            InitializeComponent();
            refresh();
            this.ComboWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategory));
            this.ComboStatusSelector.ItemsSource = Enum.GetValues(typeof(BO.ParcelStatuses));
        }

        public ParcelListWindow(IBL myBl, List<ParcelAtCustomer> parcels, string v)
        {
            this.myBl = myBl;
            InitializeComponent();
            //customerLable.Content = $"Parcel(s) {v} customer";
            parcelsView.ItemsSource = parcels;
            parcelsView.IsEnabled = false;
           
        }

        private void refresh()
        {
            var pacelsList = myBl.GetParcels();
            parcelsView.ItemsSource = pacelsList;
        }
        private void parcelsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //new ParcelWindow(parcelsView.SelectedItem).Show();
        }

        #region click
        private void bottonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void bottonAdd_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow().ShowDialog();
        }
        private void ParcelView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonClearWeight_Click(sender, e);

            Border dataParcel = e.OriginalSource as Border;
            var parcel = myBl.GetParcelById(((ParcelInList)dataParcel.DataContext).Id);
            ParcelWindow parcelWindow = new(parcel);

            parcelWindow.ShowDialog();
        }
        #endregion

        #region grouping
        private void GroupingSender_Click(object sender, RoutedEventArgs e)
        {
            refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsView.ItemsSource);
            PropertyGroupDescription groupDescription = new("SenderName");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void GroupingTarget_Click(object sender, RoutedEventArgs e)
        {
            refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsView.ItemsSource);
            PropertyGroupDescription groupDescription = new("TargetName");
            view.GroupDescriptions.Add(groupDescription);
        }
        #endregion

        #region filtred
        private void ComboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ComboWeightSelector.SelectedItem != null)
            {
                WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;

                if (ComboStatusSelector.SelectedItem == null)
                {
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Weight == Weight);

                }
                else
                {
                    ParcelStatuses status = (ParcelStatuses)ComboStatusSelector.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels();
            }
        }
        private void ComboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboStatusSelector.SelectedItem != null)
            {
                ParcelStatuses status = (ParcelStatuses)ComboStatusSelector.SelectedItem;
                if (ComboWeightSelector.SelectedItem == null)
                {
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status);
                }
                else
                {
                    WeightCategory Weight = (WeightCategory)ComboWeightSelector.SelectedItem;
                    this.parcelsView.ItemsSource = myBl.GetParcels().Where(x => x.Status == status && x.Weight == Weight);
                }
            }
            else
            {
                this.parcelsView.ItemsSource = myBl.GetParcels();
            }
        }
        private void ButtonClearWeight_Click(object sender, RoutedEventArgs e)
        {
            this.ComboWeightSelector.SelectedItem = null;
        }
        private void ButtonClearStatus_Click(object sender, RoutedEventArgs e)
        {
            ComboStatusSelector.SelectedItem = null;
        }
        #endregion
    }
}
