using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.UI.Pages;
using Unity.Lifetime;
using AutoMapper;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Managers;
using EmployeeManagement.UI.Mappings;
using Unity;

namespace EmployeeManagement.UI.DI
{
    public class RegistrationUnity
    {
        public static IUnityContainer Setup()
        {
            var container = new UnityContainer();

            container.RegisterType<ManagementContext>();

            container.RegisterType<AuthorizationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<UserService>();
            container.RegisterType<DepartmentService>();
            container.RegisterType<EmployeeService>();
            container.RegisterType<SettingsService>();

            container.RegisterType<MainWindow>();
            container.RegisterType<TrayWindow>();
            container.RegisterType<AuthorizationWindow>();

            container.RegisterType<DepartmentsPage>();
            container.RegisterType<HomePage>();
            container.RegisterType<SettingsPage>();

            container.RegisterType<TrayViewModel>();
            container.RegisterType<AuthorizationViewModel>();
            container.RegisterType<EmployeeDetailsViewModel>();
            container.RegisterType<EmployeeListViewModel>();
            container.RegisterType<HomeViewModel>();
            container.RegisterType<SettingsViewModel>();

            container.RegisterType<UnityServiceLocator>(new ContainerControlledLifetimeManager());

            container.RegisterType<WindowFactory.WindowFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<ModelViewFactory>(new ContainerControlledLifetimeManager());

            container.RegisterType<NavigationManager>(new ContainerControlledLifetimeManager());

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new UIMappingProfile());
                c.AddProfile(new DomainMappingProfile());
            });

            var mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            return container;
        }
    }
}
