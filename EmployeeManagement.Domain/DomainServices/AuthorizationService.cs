using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;

namespace EmployeeManagement.Domain.DomainServices
{
    public class AuthorizationService : IAuthorizationService
    {
        private UserModel _currentUser;

        private readonly IRegistryHelper _registryHelper;

        private readonly IUserService _userService;

        public bool IsLogged => _currentUser != null;
        public bool IsRemembered { get; set; }

        public AuthorizationService(IUserService userService, IRegistryHelper registryHelper)
        {
            _userService = userService;
            _registryHelper = registryHelper;
        }

        public async Task LogInAsync(string login, string password, bool rememberMe)
        {
            _currentUser = await _userService.GetUserModelAsync(login, password);

            if (_currentUser == null) return;
            if (!rememberMe) return;
            IsRemembered = true;
            _registryHelper.SetData("Login", login);
            _registryHelper.SetData("Password", password);
        }

        public void LogOut()
        {
            _currentUser = null;
            if (IsRemembered)
            {
                _registryHelper.RemoveData("Login", "Password");
            }
        }

        public async Task<bool> IsAuthorized()
        {
            _currentUser = await _userService.GetUserModelAsync(_registryHelper.GetData("Login"), _registryHelper.GetData("Password"));
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
