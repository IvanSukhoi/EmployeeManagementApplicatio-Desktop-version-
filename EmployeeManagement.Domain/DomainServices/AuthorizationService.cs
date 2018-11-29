using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;

namespace EmployeeManagement.Domain.DomainServices
{
    public class AuthorizationService : IAuthorizationService
    {
        private UserModel _currentUser;
        private readonly IRegistryHelper _registryHelper;
        private readonly IUserService _userService;
        private readonly IAuthorizationManager _authorizationManager;

        public bool IsLogged => _currentUser != null;
        public bool IsRemembered { get; set; }

        public AuthorizationService(IUserService userService, IRegistryHelper registryHelper, IAuthorizationManager authorizationManager)
        {
            _userService = userService;
            _registryHelper = registryHelper;
            _authorizationManager = authorizationManager;
        }

        public async Task LogInAsync(string login, string password, bool rememberMe)
        {
            await _authorizationManager.SetAuthorizationAsync(new UserModel
            {
                Login = login,
                Password = password
            });

            if (_authorizationManager.IsAuthorization())
            {
                _currentUser = await _userService.GetByLoginAsync(login);
            }

            if(_currentUser == null) return;
            if (!rememberMe) return;
            IsRemembered = true;
        }

        public void LogOut()
        {
            _currentUser = null;
            if (IsRemembered)
            {
                _registryHelper.RemoveData("RefreshToken");
            }
        }

        public async Task<bool> IsAuthorized()
        {
            _authorizationManager.UpdateRefreshTokenHandler += UpdateRefreshToken;
            var refreshToken = _registryHelper.GetData("RefreshToken");

            if (refreshToken != null)
            {
                await _authorizationManager.SetAuthorizationAsync(refreshToken);
            }

            if (_authorizationManager.IsAuthorization())
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

        public void UpdateRefreshToken(object sender, EventArgs eventArgs)
        {
            var refToken = _registryHelper.GetData("RefreshToken");

            if (refToken != null)
            {
                _registryHelper.RemoveData("RefreshToken");
            }

            _registryHelper.SetData("RefreshToken", _authorizationManager.GetRefreshToken());
        }
    }
}
