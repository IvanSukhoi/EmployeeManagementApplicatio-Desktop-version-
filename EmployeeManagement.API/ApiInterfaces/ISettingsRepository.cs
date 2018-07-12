using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface ISettingsRepository
    {
        Task<SettingsModel> GetByUserIdAsync(int id);
        Task SaveAsync(SettingsModel settingsModel);
    }
}
