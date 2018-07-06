using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Helpers
{
    public class WebClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public WebClient()
        {
            _baseUrl = "http://localhost:81/api/";
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            SetRequestHeaders();
            var response = await _httpClient.GetAsync(_baseUrl + url);

            if (!response.IsSuccessStatusCode) return null;
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T o) where T : class 
        {
            SetRequestHeaders();
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + url, o);

            if (!response.IsSuccessStatusCode) return null;
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            SetRequestHeaders();
            var response = await _httpClient.DeleteAsync(_baseUrl + url);

            if (!response.IsSuccessStatusCode) return null;
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T o) where T : class 
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl + url, o);

            if (!response.IsSuccessStatusCode) return null;
            return response;
        }

        public void SetRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
