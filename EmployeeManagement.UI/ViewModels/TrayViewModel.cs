using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IApplicationService _applicationService;
        private readonly IWindowService _windowService;

        private readonly IWindowFactory _windowFactory;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(IAuthorizationService authorizationService, IWindowFactory windowFactory, IApplicationService applicationService, IWindowService windowService)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _applicationService = applicationService;
            _windowService = windowService;
            TransitionToMainCommand = new DelegateCommandAsync(ExecuteTransitionToMainAsync);
            TransitionToAuthorizationCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToAuthorization);
            TransitionToExitCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToExit);
        }

        public bool IsLogged => _authorizationService.IsLogged;

        public async Task InitAsync()
        {
            if (await _authorizationService.IsAuthorized())
            {
                await _windowService.CreateMainWindowAsync();
            }
            else
            {
                _windowService.CreateAuthorizationWindow();
            }
            OnPropertyChanged(nameof(IsLogged));
        }

        public async Task ExecuteTransitionToMainAsync(object parametr)
        {
             await _windowService.CreateMainWindowAsync();
        }

        public void ExecuteTransitionToExit(object parametr)
        {
             _applicationService.Shutdown();
        }

        public void ExecuteTransitionToAuthorization(object parametr)
        {
            if (!_authorizationService.IsLogged)
            {
                _windowService.CreateAuthorizationWindow();
                OnPropertyChanged(nameof(IsLogged));
            }
            else
            {
                _authorizationService.LogOut();
                _windowFactory.Close<MainWindow>();
                OnPropertyChanged(nameof(IsLogged));
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
