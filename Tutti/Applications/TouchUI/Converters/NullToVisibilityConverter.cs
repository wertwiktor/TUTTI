using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace TouchUI.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public bool UseCollapsed { get; set; }
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(UseCollapsed)
            {
                if(Invert)
                {
                    return value == null ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    return value == null ? Visibility.Collapsed : Visibility.Visible;
                }           
            }
            else
            {
                if (Invert)
                {
                    return value == null ? Visibility.Visible : Visibility.Hidden;
                }
                else
                {
                    return value == null ? Visibility.Hidden : Visibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
