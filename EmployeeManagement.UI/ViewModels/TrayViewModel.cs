using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel: BindableBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IApplicationService _applicationService;
        private readonly IWindowService _windowService;

        private readonly IWindowFactory _windowFactory;

        public ICommand TransitionToMainCommand { protected set; get; }
        public ICommand TransitionToExitCommand { protected set; get; }
        public ICommand TransitionToAuthorizationCommand { protected set; get; }


        public TrayViewModel(IAuthorizationService authorizationService, IWindowFactory windowFactory, IApplicationService applicationService, IWindowService windowService)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _applicationService = applicationService;
            _windowService = windowService;
            TransitionToMainCommand = new DelegateCommand(async () => await ExecuteTransitionToMainAsync());
            TransitionToAuthorizationCommand = new DelegateCommand(ExecuteTransitionToAuthorization);
            TransitionToExitCommand = new DelegateCommand(ExecuteTransitionToExit);
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
            RaisePropertyChanged(nameof(IsLogged));
        }

        public async Task ExecuteTransitionToMainAsync()
        {
             await _windowService.CreateMainWindowAsync();
        }

        public void ExecuteTransitionToExit()
        {
             _applicationService.Shutdown();
        }

        public void ExecuteTransitionToAuthorization()
        {
            if (!_authorizationService.IsLogged)
            {
                _windowService.CreateAuthorizationWindow();
                RaisePropertyChanged(nameof(IsLogged));
            }
            else
            {
                _authorizationService.LogOut();
                _windowFactory.Close<MainWindow>();
                RaisePropertyChanged(nameof(IsLogged));
            }
        }
    }
}
