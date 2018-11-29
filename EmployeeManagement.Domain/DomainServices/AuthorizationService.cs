using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using EmployeeManagement.Domain.DomainInterfaces;

namespace EmployeeManagement.Domain.DomainServices
{
    public class AuthorizationService : IAuthorizationService
    {
        private UserModel _currentUser;

        private readonly IRegistryManager _registryManager;
        private readonly IUserService _userService;
        private readonly IAuthorizationManager _authorizationManager;

        public bool IsLogged => _currentUser != null;
        public bool IsRemembered { get; set; }

        public AuthorizationService(IUserService userService, IRegistryManager registryManager, IAuthorizationManager authorizationManager)
        {
            _userService = userService;
            _registryManager = registryManager;
            _authorizationManager = authorizationManager;
        }

        public async Task LogInAsync(string login, string password, bool rememberMe)
        {
            await _authorizationManager.SetAuthorizationAsync(new UserModel
            {
                Login = login,
                Password = password
            });

            if (_authorizationManager.IsAuthorized())
            {
                _currentUser = await _userService.GetByLoginAsync(login);
            }

            if(_currentUser == null) return;
            if (!rememberMe) return;
            IsRemembered = true;

            _registryManager.SetData(SettingsConfiguration.RegistrySettings.RefreshToken, 
                _authorizationManager.GetRefreshToken());
        }

        public void LogOut()
        {
            _currentUser = null;
            if (IsRemembered)
            {
                _registryManager.RemoveData(SettingsConfiguration
                    .RegistrySettings
                    .RefreshToken);
            }
        }

        public async Task<bool> IsAuthorized()
        {
            var refreshToken = _registryManager.GetData(SettingsConfiguration
                .RegistrySettings
                .RefreshToken);

            if (refreshToken != null)
            {
                await _authorizationManager.SetAuthorizationAsync(refreshToken);
            }

            if (_authorizationManager.IsAuthorized())
            {
                _currentUser = await _userService.GetByRefreshTokenAsync(refreshToken);
            }

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
