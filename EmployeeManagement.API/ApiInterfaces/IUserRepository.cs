using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserModelAsync(string login, string password);
    }
}
