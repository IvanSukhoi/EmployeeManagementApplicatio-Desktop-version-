using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.API.Helpers;
using EmployeeManagement.Contacts.Models;

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
            var response = await _webClient.PostAsync("User/GetUserModel", new[] { login, password });

            return await (response?.Content).ReadAsAsync<UserModel>();
        }
    }
}
