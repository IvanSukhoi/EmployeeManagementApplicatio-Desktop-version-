using System;
using System.Globalization;
using System.Windows.Data;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.Converters
{
    public class ThemeToBoolConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var theme = (Theme)value;
                if (parameter != null && theme == (Theme)parameter)
                    return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
