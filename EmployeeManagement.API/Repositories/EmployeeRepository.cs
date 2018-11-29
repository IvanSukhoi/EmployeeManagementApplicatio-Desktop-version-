using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        private readonly IWebClient _webClient;

        public EmployeeRepository(IWebClient webClient, IAuthorizationManager authorizationManager) : base(webClient, authorizationManager)
        {
            _webClient = webClient;
        }

        public async Task<List<EmployeeModel>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _webClient.GetAsync<List<EmployeeModel>>(SettingsConfiguration.ApiUrls.GetEmployeeByDepartmentIdUrl + departmentId);
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployeeUrl + id);
        }

        public async Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel)
        {
            return await _webClient.PostAsync<EmployeeModel, EmployeeModel>(SettingsConfiguration.ApiUrls.GetEmployeeUrl, employeeModel);
        }

        public async Task SaveAsync(EmployeeModel employeeModel)
        {
            if (employeeModel != null)
                await _webClient.PutAsync(SettingsConfiguration.ApiUrls.GetEmployeeUrl, employeeModel);
        }
       
        public async Task DeleteAsync(int id)
        {
            await _webClient.DeleteAsync(SettingsConfiguration.ApiUrls.GetEmployeeUrl + id);
        }
    }
}
