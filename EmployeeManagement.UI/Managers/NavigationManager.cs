using System.Threading.Tasks;
using System.Windows.Navigation;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.UI.DI;
using EmployeeManagement.UI.Pages;

namespace EmployeeManagement.UI.Managers
{
    public class NavigationManager
    {
        private NavigationService _navigationService;
        private readonly UnityServiceLocator _unityServiceLocator;

        public NavigationManager(UnityServiceLocator unityServiceLocator)
        {
            _unityServiceLocator = unityServiceLocator;
        }

        public void SetNavigationService(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task Navigate(Contracts.Enums.Pages page, Departments department)
        {
            switch (page)
            {
                case Contracts.Enums.Pages.HomePage:
                    var homePage = _unityServiceLocator.GetInstance<HomePage>();
                    await homePage.Init();
                    _navigationService.Navigate(homePage);
                    break;

                case Contracts.Enums.Pages.EmployeeListPage:
                    var employeeListPage = _unityServiceLocator.GetInstance<DepartmentsPage>();
                    await employeeListPage.InitAsync(department);
                    _navigationService.Navigate(employeeListPage);
                    break;

                case Contracts.Enums.Pages.SettingsPage:
                    var settingsPage = _unityServiceLocator.GetInstance<SettingsPage>();
                    await settingsPage.InitAsync();
                    _navigationService.Navigate(settingsPage);
                    break;

                default:
                    var defaultPage = _unityServiceLocator.GetInstance<HomePage>();
                    await defaultPage.Init();
                    _navigationService.Navigate(defaultPage);
                    break;
            }
        }
    }
}