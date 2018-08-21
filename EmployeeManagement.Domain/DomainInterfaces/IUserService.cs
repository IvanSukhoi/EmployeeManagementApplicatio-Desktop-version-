using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IUserService
    {
        Task<UserModel> GetByLoginAsync(string login);
        Task<UserModel> GetByRefreshTokenAsync(string refreshToken);
    }
}
