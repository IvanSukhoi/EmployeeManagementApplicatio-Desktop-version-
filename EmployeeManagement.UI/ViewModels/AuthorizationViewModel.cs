using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.Interfaces;

namespace EmployeeManagement.UI.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private readonly AuthorizationService _authorizationService;
        private readonly IWindow _currentWindow;

        public string Login { get; set; }
        public string Password { get; set; }
        public IDelegateCommand LogInCommand { protected set; get; }

        public AuthorizationViewModel(IWindow currentWindow, UserService userService, AuthorizationService authorizationService)
        {
            _currentWindow = currentWindow;
            _userService = userService;
            _authorizationService = authorizationService;
            LogInCommand = new DelegateCommand.DelegateCommand(ExecutePrintResultAuthorization);
        }

        void ExecutePrintResultAuthorization(object parametr)
        {
            _authorizationService.LogIn(Login, Password);
            if (_authorizationService.IsLogged)
            {
                MessageBox.Show("Successful Authorization!", "Authorization", MessageBoxButton.OK);
                _currentWindow.CloseWindow();
            }
            else
            {
                MessageBox.Show("Failed Authorization!", "Authorization");
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
