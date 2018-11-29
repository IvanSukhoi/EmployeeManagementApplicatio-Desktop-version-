using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Token;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;

namespace EmployeeManagement.API.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IWebClient _webClient;
        private readonly IRegistryManager _registryManager;

        private JsonWebToken _jsonWebToken;

        public AuthorizationManager(IWebClient webClient, IRegistryManager registryManager)
        {
            _webClient = webClient;
            _registryManager = registryManager;
        }

        public async Task SetAuthorizationAsync(UserModel userModel)
        {
            _jsonWebToken 
                = await _webClient.PostAsync<JsonWebToken, UserModel>(SettingsConfiguration.ApiUrls.GetToken, 
                    userModel, false);
        }

        public async Task SetAuthorizationAsync(string refreshToken)
        {
            _jsonWebToken =
                await _webClient.PostAsync<JsonWebToken, string>(SettingsConfiguration.ApiUrls.GetRefreshToken,
                    refreshToken, false);
         
            UpdateRefreshToken();
        }

        public async Task UpdateAuthorizationAsync() 
        {
            _jsonWebToken = await _webClient.PostAsync<JsonWebToken, string>(
                SettingsConfiguration.ApiUrls.GetRefreshToken, _jsonWebToken.RefreshToken, false);

            UpdateRefreshToken();
        }

        public bool IsAuthorized()
        {
            return _jsonWebToken != null;
        }

        public bool IsValidAccessToken()
        {
            return _jsonWebToken.Expires.CompareTo(DateTime.UtcNow) >= 0;
        }

        public string GetRefreshToken()
        {
            return _jsonWebToken.RefreshToken;
        }

        public string GetAccessToken()
        {
            return _jsonWebToken.AccessToken;
        }

        private void UpdateRefreshToken()
        {
            var refreshToken = SettingsConfiguration.RegistrySettings.RefreshToken;

            var refToken = _registryManager.GetData(refreshToken);

            if (refToken != null)
            {
                _registryManager.RemoveData(refreshToken);
            }

            _registryManager.SetData(refreshToken, _jsonWebToken.RefreshToken);
        }
    }
}
