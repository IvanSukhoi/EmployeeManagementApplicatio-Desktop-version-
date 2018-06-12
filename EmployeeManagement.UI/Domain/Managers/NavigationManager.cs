using System.Windows.Navigation;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.DI;
using EmployeeManagement.UI.Pages;

namespace EmployeeManagement.UI.Domain.Managers
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

        public void Navigate(Pages.Pages page, Departments department)
        {
            if (page == Pages.Pages.HomePage)
            {
                var homePage = _unityServiceLocator.GetInstance<HomePage>();
                _navigationService.Navigate(homePage);
            }
            else
            {
                var employeeListPage = _unityServiceLocator.GetInstance<ListEmployeePage>();
                employeeListPage.Init(department);

                _navigationService.Navigate(employeeListPage);
            }
        }
    }
}