using System.Threading.Tasks;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class UserRepository
    {
        private readonly WebClient _webClient;

        public UserRepository(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<UserModel> GetUserModelAsync(string login, string password)
        {
            return await _webClient.PostAsync<UserModel, string[]>("User/GetUserModel", new[] { login, password });
        }
    }
}
