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
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private readonly AuthorizationService _authorizationService;

        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public IDelegateCommand LogInCommand { protected set; get; }

        private readonly WindowFactory _windowFactory;

        public AuthorizationViewModel(AuthorizationService authorizationService, WindowFactory windowFactory)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            LogInCommand = new DelegateCommand<object>(ExecutePrintResultAuthorization);
        }

        void ExecutePrintResultAuthorization<T>(T parametr)
        {
            _authorizationService.LogIn(Login, Password, RememberMe);
            if (_authorizationService.IsLogged)
            {
                MessageBox.Show("Successful Authorization!", "Authorization", MessageBoxButton.OK);
                _windowFactory.Close<AuthorizationWindow>();
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
