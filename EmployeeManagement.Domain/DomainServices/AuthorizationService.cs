using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.Helpers;

namespace EmployeeManagement.Domain.DomainServices
{
    public class AuthorizationService
    {
        private UserModel _currentUser;
        public bool IsLogged => _currentUser != null;
        public bool IsRemembered { get; set; }

        private readonly UserService _userService;

        public AuthorizationService(UserService userService)
        {
            _userService = userService;
        }

        public async Task LogInAsync(string login, string password, bool rememberMe)
        {
            _currentUser = await _userService.GetUserModelAsync(login, password);

            if (_currentUser == null) return;
            if (!rememberMe) return;
            IsRemembered = true;
            RegistryHelper.SetData("Login", login);
            RegistryHelper.SetData("Password", password);
        }

        public void LogOut()
        {
            _currentUser = null;
            if (IsRemembered)
            {
                RegistryHelper.RemoveData("Login", "Password");
            }
        }

        public async Task<bool> IsAuthorized()
        {
            _currentUser = await _userService.GetUserModelAsync(RegistryHelper.GetData("Login"), RegistryHelper.GetData("Password"));
            if (_currentUser != null)
            {
                IsRemembered = true;
            }

            return IsLogged;
        }

        public UserModel GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
