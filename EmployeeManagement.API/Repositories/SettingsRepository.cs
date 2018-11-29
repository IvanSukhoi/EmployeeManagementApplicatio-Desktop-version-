using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;

namespace EmployeeManagement.API.Repositories
{
    public class SettingsRepository : BaseRepository, ISettingsRepository
    {
        private readonly IWebClient _webClient;

        public SettingsRepository(IWebClient webClient, IAuthorizationManager authorizationManager) : base(webClient, authorizationManager)
        {
            _webClient = webClient;
        }

        public async Task<SettingsModel> GetByUserIdAsync(int id)
        {
            return await _webClient.GetAsync<SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings + id);
        }

        public async Task SaveAsync(SettingsModel settingsModel)
        {
            if (settingsModel != null)
            {
                await _webClient.PostAsync<SettingsModel, SettingsModel>(SettingsConfiguration.ApiUrls.GetSettings, settingsModel);
            }
        }
    }
}
