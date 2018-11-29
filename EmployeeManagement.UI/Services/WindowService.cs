using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Events;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;
using Prism.Events;

namespace EmployeeManagement.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly ISettingsHelper _settingsHelper;
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IWindowFactory _windowFactory;
        private readonly IEventAggregator _eventAggregator;

        public WindowService(ISettingsHelper settingsHelper, ISettingsService settingsService, IAuthorizationService authorizationService, IWindowFactory windowFactory, IEventAggregator eventAggregator)
        {
            _settingsHelper = settingsHelper;
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _eventAggregator = eventAggregator;
        }

        public async Task CreateMainWindowAsync()
        {
            var settings = await _settingsService.GetByUserIdAsync(_authorizationService.GetCurrentUser().Id);

            _settingsHelper.SetLanguage(settings);
            _settingsHelper.SetTheme(settings);

            var mainWindow = _windowFactory.Create<MainWindow>();
            _eventAggregator.GetEvent<UpdateMainWindowEvent>().Publish(mainWindow);

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
