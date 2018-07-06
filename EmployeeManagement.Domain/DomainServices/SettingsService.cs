using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class SettingsService
    {
        private readonly SettingsRepository _settingsRepository;

        public SettingsService(SettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SettingsModel> GetByIdAsync(int id)
        {
            return await _settingsRepository.GetByIdAsync(id);
        }

        public async Task SaveAsync(SettingsModel settingsModel)
        {
            await _settingsRepository.SaveAsync(settingsModel);
        }
    }
}
