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
        public ShowMapWindow(double latitude, double longitude)
        {
            InitializeComponent();

            try
            {
                var googleMapsAddress = $"https://www.google.co.il//maps/@{latitude},{longitude},18z?hl=iw";


                //var bingMapsAddress = $"https://www.bing.com/maps?cp={latitude}~{longitude}&lvl=18";

                ShowMap.Source = new Uri(googleMapsAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't load the map of station! \n" + ex.Message, "map Loading Error!");
            }

            #region K
            //var k = "AtbpkGlznerExttC1tAEa7wPmubvzBDQa4Byq33BCkde0PKsuOV2PelJw_Zvnx1-";
            //ShowMap.Source = new Uri($@"http://dev.virtualearth.net/REST/v1/Locations/{latitude},{longitude}?includeEntityTypes=countryRegion&o=xml&key={k}");
            #endregion
        }
    }
}
