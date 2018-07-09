using System.Threading.Tasks;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

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
            return await _webClient.GetAsync<SettingsModel>("Settings/GetById/" + id);
        }

        public async Task SaveAsync(SettingsModel settingsModel)
        {
            if (settingsModel != null)
            {
                await _webClient.PostAsync<SettingsModel, SettingsModel>("Settings/Save", settingsModel);
            }
        }
    }
}
