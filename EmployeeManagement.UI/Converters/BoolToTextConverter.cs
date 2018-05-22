using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeManagement.UI.Converters
{
    public class SwitchBindingExtension : Binding
    {
        public SwitchBindingExtension()
        {
            Initialize();
        }

        public SwitchBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        public SwitchBindingExtension(string path, object valueIfTrue, object valueIfFalse)
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
            public BoolToTextConverter(SwitchBindingExtension switchExtension)
            {
                _switch = switchExtension;
            }

            private readonly SwitchBindingExtension _switch;

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    bool b = System.Convert.ToBoolean(value);
                    return b ? _switch.ValueIfTrue : _switch.ValueIfFalse;
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
