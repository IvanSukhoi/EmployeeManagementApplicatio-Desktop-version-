using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.Windows;
using Unity.ServiceLocation;
using EmployeeManagement.UI.CustomLifetimeManager;

namespace EmployeeManagement.UI.ViewModels
{
    public class TrayViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;
        private readonly UnityServiceLocator _unityServiceLocator;
        private readonly CustomWindowFactory _windowFactory;

        public IDelegateCommand TransitionToMainCommand { protected set; get; }
        public IDelegateCommand TransitionToExitCommand { protected set; get; }
        public IDelegateCommand TransitionToAuthorizationCommand { protected set; get; }

        public TrayViewModel(AuthorizationService authorizationService, UnityServiceLocator unityServiceLocator, CustomWindowFactory windowFactory)
        {
            _authorizationService = authorizationService;
            _unityServiceLocator = unityServiceLocator;
            _windowFactory = windowFactory;
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
             Application.Current.Shutdown();
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
            var mainWindow = _windowFactory.Create<MainWindow>();
            //var mainWindow = _unityServiceLocator.GetInstance<MainWindow>();
            mainWindow.Show();
            //mainWindow.Close();
        }

        public void CreateAuthorizationWindow()
        {
            if (!IsOpenWindow)
            {
                IsOpenWindow = true;
                var authorizationWindow = _unityServiceLocator.GetInstance<AuthorizationWindow>();
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
