using System;
using EmployeeManagement.API.ApiInterfaces;

namespace EmployeeManagement.API.TokenProvider
{
    public class TokenProvider : ITokenProvider
    {
        public JsonWebToken JsonWebToken { get; set; }

        public bool IsValidAccessToken()
        {
            return JsonWebToken.Expires.CompareTo(DateTime.UtcNow) >= 0;
        }

        public bool IsNotNullJsonWebToken()
        {
            return JsonWebToken != null;
        }
    }
}
