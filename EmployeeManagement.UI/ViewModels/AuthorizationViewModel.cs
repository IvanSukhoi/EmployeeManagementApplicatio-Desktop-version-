using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        private readonly WindowFactory _windowFactory;

        public IDelegateCommand LogInCommand { protected set; get; }

        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public AuthorizationViewModel(AuthorizationService authorizationService, WindowFactory windowFactory)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            LogInCommand = new DelegateCommandAsync(ExecutePrintResultAuthorization);
        }

        async Task ExecutePrintResultAuthorization<T>(T parametr)
        {
            await _authorizationService.LogInAsync(Login, Password, RememberMe);
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
