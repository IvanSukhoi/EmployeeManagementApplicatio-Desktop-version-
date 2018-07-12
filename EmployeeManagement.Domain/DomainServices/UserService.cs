using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserModelAsync(string login, string password)
        {
            return await _userRepository.GetUserModelAsync(login, password);
        }
    }
}
