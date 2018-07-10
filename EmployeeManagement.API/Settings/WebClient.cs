using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;

namespace EmployeeManagement.API.Settings
{
    public class WebClient : IWebClient
    {
        public async Task<T> GetAsync<T>(string url) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.GetAsync(SettingsConfiguration.GetBaseUrl() + url);

                if (!response.IsSuccessStatusCode) return null;
                {
                    return await (response?.Content).ReadAsAsync<T>();
                }
            }
        }

        public async Task<TK> PostAsync<TK, T>(string url, T o) where TK: class  
                                                              where T: class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.PostAsJsonAsync(SettingsConfiguration.GetBaseUrl() + url, o);

                if (!response.IsSuccessStatusCode) return null;
                {
                    return await (response?.Content).ReadAsAsync<TK>();
                }
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.DeleteAsync(SettingsConfiguration.GetBaseUrl() + url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response;
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T o) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.PostAsJsonAsync(SettingsConfiguration.GetBaseUrl() + url, o);

                if (!response.IsSuccessStatusCode) return null;
                return response;
            }
        }

        public void SetHttpClient(HttpClient httpClient)
        {
             httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
