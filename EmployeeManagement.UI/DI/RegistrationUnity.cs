using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Domain.Managers;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.UI.Pages;
using Unity.Lifetime;
using AutoMapper;
using EmployeeManagement.UI.Controls;
using EmployeeManagement.UI.Mappings;
using Unity;

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
            container.RegisterType<EmployeeDetailsViewModel>();
            container.RegisterType<EmployeeListViewModel>();

            container.RegisterType<UnityServiceLocator>(new ContainerControlledLifetimeManager());

            container.RegisterType<WindowFactory.WindowFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<NavigationManager>(new ContainerControlledLifetimeManager());

            container.RegisterType<EmployeeListControl>();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new DomainMappingProfile());
            });

            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            container.RegisterType<ModelViewFactory>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
