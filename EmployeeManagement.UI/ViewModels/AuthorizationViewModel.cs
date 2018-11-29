using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;
using Prism.Commands;

namespace EmployeeManagement.UI.ViewModels
{
    public class AuthorizationViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IWindowFactory _windowFactory;
        private readonly IDialogService _dialogService;

        public ICommand LogInCommand { protected set; get; }

        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public AuthorizationViewModel(IAuthorizationService authorizationService, IWindowFactory windowFactory, IDialogService dialogService)
        {
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _dialogService = dialogService;
            LogInCommand = new DelegateCommand(async () => await ExecutePrintResultAuthorizationAsync());
        }

        public async Task ExecutePrintResultAuthorizationAsync()
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
    }
}
