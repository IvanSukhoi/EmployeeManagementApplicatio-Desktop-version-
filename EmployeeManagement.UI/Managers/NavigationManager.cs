using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.UI.Pages;
using EmployeeManagement.UI.UiInterfaces;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace EmployeeManagement.UI.Managers
{
    public class NavigationManager : INavigationManager
    {
        private readonly UnityServiceLocator _unityServiceLocator;
        private readonly IRegionManager _regionManager;

        public NavigationManager(UnityServiceLocator unityServiceLocator, IRegionManager regionManager)
        {
            _unityServiceLocator = unityServiceLocator;
            _regionManager = regionManager;
        }

        public async Task Navigate(Contracts.Enums.Pages page, Departments department)
        {
            var contentRegion = _regionManager.Regions["ContentRegion"];

            switch (page)
            {
                case Contracts.Enums.Pages.HomePage:
                    var homePage = _unityServiceLocator.GetInstance<HomePage>();
                    await homePage.Init();

                    contentRegion.Add(homePage);
                    contentRegion.Activate(homePage);
                    break;

                case Contracts.Enums.Pages.EmployeeListPage:
                    var employeeListPage = _unityServiceLocator.GetInstance<DepartmentsPage>();
                    await employeeListPage.InitAsync(department);

                    contentRegion.Add(employeeListPage);
                    contentRegion.Activate(employeeListPage);
                    break;

                case Contracts.Enums.Pages.SettingsPage:
                    var settingsPage = _unityServiceLocator.GetInstance<SettingsPage>();
                    await settingsPage.InitAsync();

                    contentRegion.Add(settingsPage);
                    contentRegion.Activate(settingsPage);
                    break;

                default:
                    var defaultPage = _unityServiceLocator.GetInstance<HomePage>();
                    await defaultPage.Init();

                    contentRegion.Add(defaultPage);
                    contentRegion.Activate(defaultPage);
                    break;
            }
        }
    }
}