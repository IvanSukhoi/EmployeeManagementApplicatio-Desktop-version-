using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.API.WebClient;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Helpers;
using EmployeeManagement.UI.Pages;
using Unity.Lifetime;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Helpers;
using EmployeeManagement.UI.Managers;
using EmployeeManagement.UI.Mappings;
using EmployeeManagement.UI.Services;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.UiInterfaces.ViewModels;
using Unity;

namespace EmployeeManagement.UI.DI
{
    public class RegistrationUnity
    {
        public static IUnityContainer Setup()
        {
            var container = new UnityContainer();

            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<ISettingsRepository, SettingsRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<IRegistryHelper, RegistryHelper>();
            container.RegisterType<ISettingsHelper, SettingsHelper>();

            container.RegisterType<IAuthorizationService, AuthorizationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<ISettingsService, SettingsService>();

            container.RegisterType<IApplicationService, ApplicationService>();
            container.RegisterType<IWindowService, WindowService>();
            container.RegisterType<IResourceManagerService, ResourceManagerService>();

            container.RegisterType<MainWindow>();
            container.RegisterType<TrayWindow>();
            container.RegisterType<AuthorizationWindow>();

            container.RegisterType<DepartmentsPage>();
            container.RegisterType<HomePage>();
            container.RegisterType<SettingsPage>();

            container.RegisterType<TrayViewModel>();
            container.RegisterType<IAuthorizationViewModel, AuthorizationViewModel>();
            container.RegisterType<EmployeeDetailsViewModel>();
            container.RegisterType<EmployeeListViewModel>();
            container.RegisterType<HomeViewModel>();
            container.RegisterType<SettingsViewModel>();
            container.RegisterType<IMainViewModel, MainViewModel>();

            container.RegisterType<UnityServiceLocator>(new ContainerControlledLifetimeManager());

            container.RegisterType<IWindowFactory, WindowFactory.WindowFactory>(new ContainerControlledLifetimeManager());

            container.RegisterType<INavigationManager, NavigationManager>(new ContainerControlledLifetimeManager());

            container.RegisterType<IMapperWrapper, MapperWrapper>(new ContainerControlledLifetimeManager());

            container.RegisterType<IWebClient, WebClient>();

            container.RegisterType<IDialogService, DialogService>();

            return container;
        }
    }
}
