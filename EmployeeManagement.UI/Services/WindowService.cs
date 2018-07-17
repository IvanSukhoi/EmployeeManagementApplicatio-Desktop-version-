using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly ISettingsHelper _settingsHelper;
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IWindowFactory _windowFactory;

        public WindowService(ISettingsHelper settingsHelper, ISettingsService settingsService, IAuthorizationService authorizationService, IWindowFactory windowFactory)
        {
            _settingsHelper = settingsHelper;
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
        }

        public async Task CreateMainWindowAsync()
        {
            var settings = await _settingsService.GetByUserIdAsync(_authorizationService.GetCurrentUser().Id);
            _settingsHelper.SetLanguage(settings);
            _settingsHelper.SetTheme(settings);

            var mainWindow = _windowFactory.Create<MainWindow>();
            await mainWindow.InitAsync();
            mainWindow.ShowWindow();
        }

        public void CreateAuthorizationWindow()
        {
            var authorizationWindow = _windowFactory.Create<AuthorizationWindow>();
            if (authorizationWindow.Visibility == Visibility.Collapsed)
            {
                authorizationWindow.ShowDialogWindow();
            }
        }
    }
}
