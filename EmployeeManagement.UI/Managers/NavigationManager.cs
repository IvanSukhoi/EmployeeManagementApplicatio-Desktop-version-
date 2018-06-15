using System.Windows.Navigation;
using EmployeeManagement.Domain.Enums;
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

        public void Navigate(Enums.Pages page, Departments department)
        {
            switch (page)
            {
                case Enums.Pages.HomePage:
                    var homePage = _unityServiceLocator.GetInstance<HomePage>();
                    homePage.Init();
                    _navigationService.Navigate(homePage);
                    break;

                case Enums.Pages.EmployeeListPage:
                    var employeeListPage = _unityServiceLocator.GetInstance<DepartmentsPage>();
                    employeeListPage.Init(department);
                    _navigationService.Navigate(employeeListPage);
                    break;

                case Enums.Pages.SettingsPage:
                    var settingsPage = _unityServiceLocator.GetInstance<SettingsPage>();
                    settingsPage.Init();
                    _navigationService.Navigate(settingsPage);
                    break;

                default:
                    var defaultPage = _unityServiceLocator.GetInstance<HomePage>();
                    defaultPage.Init();
                    _navigationService.Navigate(defaultPage);
                    break;
            }
        }
    }
}