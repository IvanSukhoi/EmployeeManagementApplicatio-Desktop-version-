using System;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;

namespace EmployeeManagement.API.Repositories
{
    public class BaseRepository
    {
        private readonly IAuthorizationManager _authorizationManager;

        public BaseRepository(IWebClient webClient, IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager;
            webClient.GetAccessTokenFunc = _authorizationManager.GetAccessToken;
            webClient.UpdateAuthorizationFunc = UpdateAuthorizationAsync;
        }

        public async Task UpdateAuthorizationAsync()
        {
            if (!_authorizationManager.IsValidAuthorization())
            {
                await _authorizationManager.UpdateAuthorizationAsync();
            }
        }

        public void  ErrorProcessing()
        { }
    }
}
