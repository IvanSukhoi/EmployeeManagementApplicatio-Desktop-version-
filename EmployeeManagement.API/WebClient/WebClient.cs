using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Settings;

namespace EmployeeManagement.API.WebClient
{
    public class WebClient : IWebClient
    {
        public Func<Task> UpdateAuthorizationFunc { get; set; }
        public Func<string> GetAccessTokenFunc { get; set; }

        public async Task<T> GetAsync<T>(string url, bool isAuthenticated = true) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                await SetHttpClient(httpClient, isAuthenticated);

                var response = await httpClient.GetAsync(SettingsConfiguration.BaseUrl + url);

                if (response.IsSuccessStatusCode)
                {
                    return await (response.Content).ReadAsAsync<T>();
                }

                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        public async Task<TK> PostAsync<TK, T>(string url, T o, bool isAuthenticated = true) where TK: class  
                                                              where T: class 
        {
            using (var httpClient = new HttpClient())
            {
                await SetHttpClient(httpClient, isAuthenticated);

                var response = await httpClient.PostAsJsonAsync(SettingsConfiguration.BaseUrl + url, o);

                if (response.IsSuccessStatusCode) 
                {
                    return await (response.Content).ReadAsAsync<TK>();
                }

                throw  new InvalidOperationException("Object with such id can not be created");
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, bool isAuthenticated = true)
        {
            using (var httpClient = new HttpClient())
            {
                await SetHttpClient(httpClient, isAuthenticated);

                var response = await httpClient.DeleteAsync(SettingsConfiguration.BaseUrl + url);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                
                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T o, bool isAuthenticated = true) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                await SetHttpClient(httpClient, isAuthenticated);

                var response = await httpClient.PutAsJsonAsync(SettingsConfiguration.BaseUrl + url, o);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }

                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        private async Task SetHttpClient(HttpClient httpClient, bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                await UpdateAuthorizationFunc.Invoke();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessTokenFunc.Invoke());
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
