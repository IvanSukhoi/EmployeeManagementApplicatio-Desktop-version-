using System.Threading.Tasks;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserModelAsync(string login, string password)
        {
            return await _userRepository.GetUserModelAsync(login, password);
        }
    }
}
