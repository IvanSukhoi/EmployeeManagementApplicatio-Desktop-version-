using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly UserService _userService;
        private readonly IWindow _currentWindow;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        private string _logInOrLogOut;

        public string LogInOrLogOut
        {
            get
            {
                _logInOrLogOut = _authorizationService.IsLogged ? "LogOut" : "LogIn";
                return _logInOrLogOut;
            }
        }

        public TrayViewModel(IWindow currentWindow, UserService userService, AuthorizationService authorizationService)
        {
            _currentWindow = currentWindow;
            _userService = userService;
            _authorizationService = authorizationService;
            TransitionToMainCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToMain);
            TransitionToAuthorizationCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToAuthorizationCommand);
            TransitionToExitCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToExit);
        }

        void ExecuteTransitionToMain(object parametr)
        {
            var mainWindow = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            mainWindow.ShowDialog();
        }

        void ExecuteTransitionToExit(object parametr)
        {
            _currentWindow.CloseWindow();
        }

        void ExecuteTransitionToAuthorizationCommand(object parametr)
        {
            if (!_authorizationService.IsLogged)
            {
                var authorizationWindow = new AuthorizationWindow();
                authorizationWindow.DataContext = new AuthorizationViewModel(authorizationWindow, _userService, _authorizationService);
                authorizationWindow.ShowDialog();
                OnPropertyChanged(nameof(LogInOrLogOut));
            }
            else
            {
                _currentWindow.CloseWindow();
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
