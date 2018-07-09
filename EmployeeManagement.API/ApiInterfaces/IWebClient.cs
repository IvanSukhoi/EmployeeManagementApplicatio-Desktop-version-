using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IWebClient
    {
        Task<T> GetAsync<T>(string url) where T : class;
        Task<TK> PostAsync<TK, T>(string url, T o) where TK : class where T : class;
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<HttpResponseMessage> PutAsync<T>(string url, T o) where T : class;
    }
}
