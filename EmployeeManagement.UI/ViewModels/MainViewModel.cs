using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Helpers;
using EmployeeManagement.UI.Managers;

namespace EmployeeManagement.UI.ViewModels
{
    public class MainViewModel
    {
        public IDelegateCommand SelectByDepartmentCommand { protected set; get; }
        public IDelegateCommand ChangeSettingsCommand { protected set; get; }

        private readonly AuthorizationService _authorizationService;
        private readonly SettingsService _settingsService;
        private readonly NavigationManager _navigationManager;

        public MainViewModel(NavigationManager navigationManager, AuthorizationService authorizationService, SettingsService settingsService)
        {
            _navigationManager = navigationManager;
            _authorizationService = authorizationService;
            _settingsService = settingsService;
            SelectByDepartmentCommand = new DelegateCommand.DelegateCommand(ExecuteSelectByDepartment);
            ChangeSettingsCommand = new DelegateCommand.DelegateCommand(ExecuteChangeSettings);
        }

        public void Init()
        {
            _navigationManager.Navigate(Enums.Pages.HomePage, Departments.NotSelected);

            var settings = _settingsService.GetById(_authorizationService.GetCurrentUser().ID);
            SettingsHelper.SetTheme(settings);
        }

        void ExecuteSelectByDepartment(object parameter)
        {
            var values = (object[])parameter;

            var page = (Enums.Pages)values[0];
            var department = (Departments) values[1];

            _navigationManager.Navigate(page, department);
        }

        void ExecuteChangeSettings(object parameter)
        {
            var page = (Enums.Pages) parameter;

            _navigationManager.Navigate(page, Departments.NotSelected);
        }
    }
}
