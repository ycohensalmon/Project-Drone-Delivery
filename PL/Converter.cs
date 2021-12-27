using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL
{
    class ButtonUpdateDroneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Drone dr = value as Drone;

            switch (dr.Status)
            {
                case DroneStatuses.Available:
                    return "Send to charge";
                case DroneStatuses.Maintenance:
                    return "Release from charge";
                case DroneStatuses.Delivery:
                    if (dr.ParcelInTravel.InTravel == true)
                        return "Collect delivery";
                    else
                        return "Delivered parcel by this drone";
            }
            return "update";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class VisibilityUpdateDroneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DroneStatuses statuses = (DroneStatuses)value;

            if (statuses == DroneStatuses.Available)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class EnableTextModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MaterialDesignThemes.Wpf.PackIconKind iconKind = (MaterialDesignThemes.Wpf.PackIconKind)value;
            if (iconKind == MaterialDesignThemes.Wpf.PackIconKind.Verified)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class BrushTextModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MaterialDesignThemes.Wpf.PackIconKind iconKind = (MaterialDesignThemes.Wpf.PackIconKind)value;
            if (iconKind == MaterialDesignThemes.Wpf.PackIconKind.Verified)
                return System.Drawing.Color.DarkBlue;
            else
                return System.Drawing.Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class VisibilityParcelInTravelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DroneStatuses statuses = (DroneStatuses)value;

            if (statuses == DroneStatuses.Delivery)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
