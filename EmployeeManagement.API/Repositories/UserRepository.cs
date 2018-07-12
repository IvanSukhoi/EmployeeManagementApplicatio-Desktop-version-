using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IWebClient _webClient;

        public UserRepository(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<UserModel> GetUserModelAsync(string login, string password)
        {
            return await _webClient.PostAsync<UserModel, string[]>(SettingsConfiguration.ApiUrls.User.GetUserModel, new[] { login, password });
        }
    }
}
