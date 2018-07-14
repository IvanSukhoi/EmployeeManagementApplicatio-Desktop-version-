using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;

namespace EmployeeManagement.API.WebClient
{
    public class WebClient : IWebClient
    {
        public async Task<T> GetAsync<T>(string url) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.GetAsync(SettingsConfiguration.BaseUrl + url);

                if (response.IsSuccessStatusCode)
                {
                    return await (response?.Content).ReadAsAsync<T>();
                }

                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        public async Task<TK> PostAsync<TK, T>(string url, T o) where TK: class  
                                                              where T: class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.PostAsJsonAsync(SettingsConfiguration.BaseUrl + url, o);

                if (response.IsSuccessStatusCode) 
                {
                    return await (response?.Content).ReadAsAsync<TK>();
                }

                throw  new InvalidOperationException("Object with such id can not be created");
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.DeleteAsync(SettingsConfiguration.BaseUrl + url);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                
                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T o) where T : class 
        {
            using (var httpClient = new HttpClient())
            {
                SetHttpClient(httpClient);
                var response = await httpClient.PutAsJsonAsync(SettingsConfiguration.BaseUrl + url, o);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }

                throw new InvalidOperationException("Object with such id does not exist");
            }
        }

        public void SetHttpClient(HttpClient httpClient)
        {
             httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
