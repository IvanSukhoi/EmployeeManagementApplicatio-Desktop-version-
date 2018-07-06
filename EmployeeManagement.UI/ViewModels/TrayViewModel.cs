using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.DI.WindowFactory;
using EmployeeManagement.UI.Helpers;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly SettingsService _settingsService;
        private readonly WindowFactory _windowFactory;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(AuthorizationService authorizationService, WindowFactory windowFactory, SettingsService settingsService)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _settingsService = settingsService;
            TransitionToMainCommand = new DelegateCommandAsync(ExecuteTransitionToMainAsync);
            TransitionToAuthorizationCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToAuthorizationCommand);
            TransitionToExitCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToExit);
        }

        public bool IsLogged => _authorizationService.IsLogged;

        public async Task InitAsync()
        {
            if (await _authorizationService.IsAuthorized())
            {
                await CreateMainWindowAsync();
            }
            else
            {
                CreateAuthorizationWindow();
            }
            OnPropertyChanged(nameof(IsLogged));
        }

        async Task ExecuteTransitionToMainAsync<T>(T parametr)
        {
             await CreateMainWindowAsync();
        }

        void ExecuteTransitionToExit(object parametr)
        {
             Application.Current.Shutdown();
        }

        void ExecuteTransitionToAuthorizationCommand(object parametr)
        {
            if (!_authorizationService.IsLogged)
            {
                CreateAuthorizationWindow();
                OnPropertyChanged(nameof(IsLogged));
            }
            else
            {
                _authorizationService.LogOut();
                _windowFactory.Close<MainWindow>();
                OnPropertyChanged(nameof(IsLogged));
            }
        }

        public async Task CreateMainWindowAsync()
        {
            SettingsHelper.SetLanguage(await _settingsService.GetByIdAsync(_authorizationService.GetCurrentUser().Id));
            var settings = await _settingsService.GetByIdAsync(_authorizationService.GetCurrentUser().Id);
            SettingsHelper.SetTheme(settings);

            var mainWindow = _windowFactory.Create<MainWindow>();
            await mainWindow.InitAsync();
            mainWindow.Show();
        }

        public void CreateAuthorizationWindow()
        {
            var authorizationWindow = _windowFactory.Create<AuthorizationWindow>();
            if (authorizationWindow.Visibility == Visibility.Collapsed)
            {
                authorizationWindow.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
