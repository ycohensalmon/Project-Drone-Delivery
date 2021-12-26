using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    class ButtonUpdateConverter : IValueConverter
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

    class VisibilityUpdateConverter : IValueConverter
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
}
