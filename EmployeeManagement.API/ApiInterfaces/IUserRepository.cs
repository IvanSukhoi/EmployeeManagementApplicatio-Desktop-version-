using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetByLoginAsync(string login);
        Task<UserModel> GetByRefreshTokenAsync(string refreshToken);
    }
}
