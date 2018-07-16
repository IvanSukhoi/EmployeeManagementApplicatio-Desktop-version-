using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface ISettingsService
    {
        Task<SettingsModel> GetByUserIdAsync(int id);
        Task SaveAsync(SettingsModel settingsModel);
    }
}
