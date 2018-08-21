using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IWebClient _webClient;

        public UserRepository(IWebClient webClient, IAuthorizationManager authorizationManager) : base(webClient, authorizationManager) 
        {
            _webClient = webClient;
        }

        public async Task<UserModel> GetByLoginAsync(string login)
        {
            return await _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUserUrl, login);
        }

        public async Task<UserModel> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _webClient.PostAsync<UserModel, string>(SettingsConfiguration.ApiUrls.GetUserByRefreshtokenUrl, refreshToken);
        }
    }
}
