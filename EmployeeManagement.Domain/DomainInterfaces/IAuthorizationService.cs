using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IAuthorizationService
    {
        Task LogInAsync(string login, string password, bool rememberMe);
        void LogOut();
        Task<bool> IsAuthorized();
        UserModel GetCurrentUser();
        bool IsLogged { get;}
        bool IsRemembered { get; set; }
    }
}
