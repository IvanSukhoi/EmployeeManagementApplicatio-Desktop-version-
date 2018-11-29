using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IAuthorizationManager
    {
        Task SetAuthorizationAsync(UserModel userModel);
        Task SetAuthorizationAsync(string refreshToken);
        Task UpdateAuthorizationAsync();
        bool IsValidAccessToken();
        bool IsAuthorized();
        string GetRefreshToken();
        string GetAccessToken();
    }
}
