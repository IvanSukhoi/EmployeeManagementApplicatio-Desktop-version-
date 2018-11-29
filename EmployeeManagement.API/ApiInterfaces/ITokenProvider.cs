using EmployeeManagement.API.TokenProvider;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface ITokenProvider
    {
        JsonWebToken JsonWebToken { get; set; }
        bool IsValidAccessToken();
        bool IsNotNullJsonWebToken();
    }
}
