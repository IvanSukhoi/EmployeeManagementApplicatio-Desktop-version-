using System;
using System.Collections.Generic;
using System.Windows;
using EmployeeManagement.UI.Interfaces;
using Unity;

namespace EmployeeManagement.UI.CustomLifetimeManager
{
    public class CustomWindowFactory
    {
        private readonly IUnityContainer _container;
        private Dictionary<Type, Window> list = new Dictionary<Type, Window>();

        public CustomWindowFactory(IUnityContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T: Window
        {
            var serviceType = typeof(T);
            if (list.ContainsKey(serviceType))
                return (T)list[serviceType];

            var window = (T)_container.Resolve(serviceType);
            window.Closed += Window_Closed;
            list.Add(serviceType, window);

            return window;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var type = sender.GetType();
            if (list.ContainsKey(type))
                list.Remove(type);
        }
    }
}