using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IWebClient
    {
        Task<T> GetAsync<T>(string url, bool isAuthenticated = true) where T : class;
        Task<TK> PostAsync<TK, T>(string url, T o, bool isAuthenticated = true) where TK : class where T : class;
        Task<HttpResponseMessage> DeleteAsync(string url, bool isAuthenticated = true);
        Task<HttpResponseMessage> PutAsync<T>(string url, T o, bool isAuthenticated = true) where T : class;
        Func<Task> UpdateAuthorizationFunc { get; set; }
        Func<string> GetAccessTokenFunc { get; set; }
    }
}
