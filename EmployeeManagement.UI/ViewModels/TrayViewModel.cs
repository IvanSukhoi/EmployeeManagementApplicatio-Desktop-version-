using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.DI.WindowFactory;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly WindowFactory _windowFactory;

        public IDelegateCommand<object> TransitionToMainCommand { protected set; get; }
        public IDelegateCommand<object> TransitionToExitCommand { protected set; get; }
        public IDelegateCommand<object> TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(AuthorizationService authorizationService, WindowFactory windowFactory)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            TransitionToMainCommand = new DelegateCommand<object>(ExecuteTransitionToMain);
            TransitionToAuthorizationCommand = new DelegateCommand<object>(ExecuteTransitionToAuthorizationCommand);
            TransitionToExitCommand = new DelegateCommand<object>(ExecuteTransitionToExit);
        }

        public bool IsLogged => _authorizationService.IsLogged;

        public void Init()
        {
            if (_authorizationService.IsAuthorized())
            {
                CreateMainWindow();
            }
            else
            {
                CreateAuthorizationWindow();
            }
            OnPropertyChanged(nameof(IsLogged));
        }

        void ExecuteTransitionToMain<T>(T parametr)
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
                _windowFactory.Close<MainWindow>();
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
