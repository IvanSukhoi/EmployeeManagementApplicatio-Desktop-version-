using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;

namespace EmployeeManagement.Domain.DomainServices
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SettingsModel> GetByUserIdAsync(int id)
        {
            return await _settingsRepository.GetByUserIdAsync(id);
        }

        public async Task SaveAsync(SettingsModel settingsModel)
        {
            await _settingsRepository.SaveAsync(settingsModel);
        }
    }
}
