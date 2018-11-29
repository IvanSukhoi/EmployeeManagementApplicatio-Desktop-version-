using System;
using System.Collections.Generic;
using CommonServiceLocator;
using Unity;

namespace EmployeeManagement.UI.DI
{
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        private readonly IUnityContainer _container;

        public UnityServiceLocator(IUnityContainer container)
        {
            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType); 
        }
    }
}
