using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using Unity;
using Unity.Lifetime;
using EmployeeManagement.UI.CustomLifetimeManager;

namespace EmployeeManagement.UI.DI
{
    public class RegistrationUnity
    {
        public static IUnityContainer Setup()
        {
            var container = new UnityContainer();

            container.RegisterType<AuthorizationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<UserService>();

            container.RegisterType<ManagementContext>();

            container.RegisterType<MainWindow>();
            container.RegisterType<TrayWindow>();

            container.RegisterType<TrayViewModel>();

            container.RegisterType<IWindow, AuthorizationWindow>();

            container.RegisterType<AuthorizationViewModel>(new ContainerControlledLifetimeManager());

            container.RegisterType<UnityServiceLocator>(new ContainerControlledLifetimeManager());

            container.RegisterType<CustomWindowFactory>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
