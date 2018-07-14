using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IUserService
    {
        Task<UserModel> GetUserModelAsync(string login, string password);
    }
}
