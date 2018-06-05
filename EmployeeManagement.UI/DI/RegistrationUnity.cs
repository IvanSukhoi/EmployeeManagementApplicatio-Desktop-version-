using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Domain.Managers;
using EmployeeManagement.UI.Pages;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using Unity;
using Unity.Lifetime;

namespace EmployeeManagement.UI.DI
{
    public class RegistrationUnity
    {
        public static IUnityContainer Setup()
        {
            var container = new UnityContainer();

            container.RegisterType<AuthorizationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<UserService>();
            container.RegisterType<DepartmentService>();
            container.RegisterType<EmployeeService>();

            container.RegisterType<ManagementContext>();

            container.RegisterType<MainWindow>();
            container.RegisterType<TrayWindow>();
            container.RegisterType<AuthorizationWindow>();

            container.RegisterType<ListEmployeePage>();
            container.RegisterType<HomePage>();

            container.RegisterType<TrayViewModel>();
            container.RegisterType<AuthorizationViewModel>();

            container.RegisterType<UnityServiceLocator>(new ContainerControlledLifetimeManager());

            container.RegisterType<WindowFactory.WindowFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<NavigationManager>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
