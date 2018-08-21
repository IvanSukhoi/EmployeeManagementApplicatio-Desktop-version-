using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        private readonly IWebClient _webClient;

        public DepartmentRepository(IWebClient webClient, IAuthorizationManager authorizationManager) : base(webClient, authorizationManager)
        {
            _webClient = webClient;
        }

        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            return await _webClient.GetAsync<List<DepartmentModel>>(SettingsConfiguration.ApiUrls.GetDepartmentUrl);
        }

        public async Task<DepartmentModel> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<DepartmentModel>(SettingsConfiguration.ApiUrls.GetDepartmentUrl + id);
        }
    }
}
