using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.WindowFactory;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly WindowFactory.WindowFactory _windowFactory;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(AuthorizationService authorizationService, WindowFactory.WindowFactory windowFactory)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            TransitionToMainCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToMain);
            TransitionToAuthorizationCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToAuthorizationCommand);
            TransitionToExitCommand = new DelegateCommand.DelegateCommand(ExecuteTransitionToExit);
        }

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
                _windowFactory.Remove(typeof(MainWindow));
                OnPropertyChanged(nameof(IsLogged));
            }
        }

        public void CreateMainWindow()
        {
            var mainWindow = _windowFactory.Create<MainWindow>();
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
