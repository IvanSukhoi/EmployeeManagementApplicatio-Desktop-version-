using System;
using System.Collections.Generic;
using System.Windows;
using EmployeeManagement.UI.Windows;
using Unity;

namespace EmployeeManagement.UI.DI.WindowFactory
{
    public class WindowFactory
    {
        private readonly IUnityContainer _container;
        private readonly Dictionary<Type, Window> _list = new Dictionary<Type, Window>();

        public WindowFactory(IUnityContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T: Window
        {
            var serviceType = typeof(T);
            if (_list.ContainsKey(serviceType))
                return (T)_list[serviceType];

            var window = (T)_container.Resolve(serviceType);
            window.Closed += Window_Closed;
            _list.Add(serviceType, window);

            return window;
        }

        public T Get<T>() where T : Window
        {
            var serviceType = typeof(T);
            if (_list.ContainsKey(serviceType))
            {
                return (T)_list[serviceType];
            }

            return null;
        }

        public void Close<T>() where T : Window
        {
            var serviceType = typeof(T);

            if (!_list.ContainsKey(serviceType)) return;

            if (serviceType == typeof(TrayWindow))
            {
                _list[serviceType].Hide();
                _list[serviceType].Close();
                return;
            }

            if (_list[serviceType].Visibility != Visibility.Visible) return;
            _list[serviceType].Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var type = sender.GetType();
            if (_list.ContainsKey(type))
                _list.Remove(type);
        }
    }
}