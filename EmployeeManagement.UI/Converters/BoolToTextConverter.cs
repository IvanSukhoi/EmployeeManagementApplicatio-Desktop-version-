using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeManagement.UI.Converters
{
    public class BoolToTextSwitchBinding : Binding
    {
        public BoolToTextSwitchBinding()
        {
            Initialize();
        }

        public BoolToTextSwitchBinding(string path)
            : base(path)
        {
            Initialize();
        }

        public BoolToTextSwitchBinding(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            Initialize();
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;
        }

        private void Initialize()
        {
            ValueIfTrue = DoNothing;
            ValueIfFalse = DoNothing;
            Converter = new BoolToTextConverter(this);
        }

        public object ValueIfTrue { get; set; }

        public object ValueIfFalse { get; set; }

        private class BoolToTextConverter : IValueConverter
        {
            public BoolToTextConverter(BoolToTextSwitchBinding boolToTextSwitch)
            {
                _boolToTextSwitch = boolToTextSwitch;
            }

            private readonly BoolToTextSwitchBinding _boolToTextSwitch;

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    return System.Convert.ToBoolean(value) ? _boolToTextSwitch.ValueIfTrue : _boolToTextSwitch.ValueIfFalse;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DoNothing;
            }
        }
    }
}
