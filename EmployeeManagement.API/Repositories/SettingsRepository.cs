using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.API.Helpers;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class SettingsRepository
    {
        private readonly WebClient _webClient;

        public SettingsRepository(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<SettingsModel> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync("Settings/GetById/" + id);

            return await (response?.Content).ReadAsAsync<SettingsModel>();
        }

        public async Task SaveAsync(SettingsModel settingsModel)
        {
            if (settingsModel != null)
            {
                await _webClient.PostAsync("Settings/Save", settingsModel);
            }
        }
    }
}
