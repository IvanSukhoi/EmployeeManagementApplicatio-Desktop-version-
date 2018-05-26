using System;
using System.Collections.Generic;
using System.Windows;
using Unity;

namespace EmployeeManagement.UI.WindowFactory
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

        public void Remove(Type type)
        {
            if (!_list.ContainsKey(type)) return;
            if (_list[type].Visibility != Visibility.Visible) return;
            _list[type].Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var type = sender.GetType();
            if (_list.ContainsKey(type))
                _list.Remove(type);
        }
    }
}