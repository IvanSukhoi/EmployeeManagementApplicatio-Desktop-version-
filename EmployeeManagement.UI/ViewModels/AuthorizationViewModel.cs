using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.ViewModels;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class AuthorizationViewModel : IAuthorizationViewModel, INotifyPropertyChanged
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IWindowFactory _windowFactory;
        private readonly IDialogService _dialogService;

        public IDelegateCommand LogInCommand { protected set; get; }

        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public AuthorizationViewModel(IAuthorizationService authorizationService, IWindowFactory windowFactory, IDialogService dialogService)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _dialogService = dialogService;
            LogInCommand = new DelegateCommandAsync(ExecutePrintResultAuthorization);
        }

        public async Task ExecutePrintResultAuthorization(object parametr)
        {
            await _authorizationService.LogInAsync(Login, Password, RememberMe);
            if (_authorizationService.IsLogged)
            {
                _dialogService.ShowMessageBox("Successful Authorization!", "Authorization", MessageBoxButton.OK);
                _windowFactory.Close<AuthorizationWindow>();
            }
            else
            {
                _dialogService.ShowMessageBox("Failed Authorization!", "Authorization");
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
