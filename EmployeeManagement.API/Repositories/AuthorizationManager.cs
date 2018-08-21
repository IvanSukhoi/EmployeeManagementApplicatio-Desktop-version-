using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.API.TokenProvider;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IWebClient _webClient;
        private JsonWebToken _jsonWebToken;
        public event EventHandler UpdateRefreshTokenHandler;

        public AuthorizationManager(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task SetAuthorizationAsync(UserModel userModel)
        {
            _jsonWebToken = await _webClient.PostAsync<JsonWebToken, UserModel>(SettingsConfiguration.ApiUrls.GetTokenUrl, userModel, false);

            OnUpdateRefreshTokenHandler();
        }

        public async Task SetAuthorizationAsync(string refreshToken)
        {
            try
            {
                _jsonWebToken =
                    await _webClient.PostAsync<JsonWebToken, string>(SettingsConfiguration.ApiUrls.GetRefreshTokenUrl,
                        refreshToken, false);

                OnUpdateRefreshTokenHandler();
            }
            catch (Exception)
            {
                _jsonWebToken = null;
            }
        }

        public async Task UpdateAuthorizationAsync() 
        {
            _jsonWebToken = await _webClient.PostAsync<JsonWebToken, string>(
                SettingsConfiguration.ApiUrls.GetRefreshTokenUrl, _jsonWebToken.RefreshToken, false);

            OnUpdateRefreshTokenHandler();
        }

        public bool IsAuthorization()
        {
            return _jsonWebToken != null;
        }

        public bool IsValidAuthorization()
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

        protected virtual void OnUpdateRefreshTokenHandler()
        {
            UpdateRefreshTokenHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
