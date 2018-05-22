using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IWindow _currentWindow;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(IWindow currentWindow, AuthorizationService authorizationService)
        {
            _currentWindow = currentWindow;
            _authorizationService = authorizationService;
            TransitionToMainCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToMain);
            TransitionToAuthorizationCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToAuthorizationCommand);
            TransitionToExitCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToExit);
        }

        public bool IsOpenWindow { get; set; }
        public bool IsLogged => _authorizationService.IsLogged;

        public void Init()
        {
            _authorizationService.IsAuthorized();
            OnPropertyChanged(nameof(IsLogged));

            if (_authorizationService.IsLogged)
            {
                CreateMainWindow();
            }
            else
            {
                CreateAuthorizationWindow();
                OnPropertyChanged(nameof(IsLogged));
            }
        }

        void ExecuteTransitionToMain(object parametr)
        {
              CreateMainWindow();
        }

        void ExecuteTransitionToExit(object parametr)
        {
            if (!IsOpenWindow)
            {
                _currentWindow.CloseWindow();
            }
        }

        void ExecuteTransitionToAuthorizationCommand(object parametr)
        {
            if (!IsOpenWindow)
            {
                if (!_authorizationService.IsLogged)
                {
                    CreateAuthorizationWindow();
                    OnPropertyChanged(nameof(IsLogged));
                }
                else
                {
                    _authorizationService.LogOut();
                    OnPropertyChanged(nameof(IsLogged));
                }
            }
        }

        public void CreateMainWindow()
        {
            if (!IsOpenWindow)
            {
                IsOpenWindow = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                IsOpenWindow = false;
            }
        }

        public void CreateAuthorizationWindow()
        {
            if (!IsOpenWindow)
            {
                IsOpenWindow = true;
                var authorizationWindow = new AuthorizationWindow();
                authorizationWindow.DataContext =
                    new AuthorizationViewModel(authorizationWindow, _authorizationService);
                authorizationWindow.ShowDialog();
                IsOpenWindow = false;
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
