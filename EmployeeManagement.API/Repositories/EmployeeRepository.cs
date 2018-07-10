using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.API.Settings;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class EmployeeRepository
    {
        private readonly IWebClient _webClient;

        public EmployeeRepository(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<List<EmployeeModel>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _webClient.GetAsync<List<EmployeeModel>>("Employee/GetByDepartmentId/" + departmentId);
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<EmployeeModel>("Employee/GetById/" + id);
        }

        public async Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel)
        {
            return await _webClient.PostAsync<EmployeeModel, EmployeeModel>("Employee/Create", employeeModel);
        }

        public async Task SaveAsync(EmployeeModel employeeModel)
        {
            if (employeeModel != null)
                await _webClient.PutAsync("Employee/Update", employeeModel);
        }
       
        public async Task DeleteAsync(int id)
        {
            await _webClient.DeleteAsync("Employee/Delete/" + id);
        }
    }
}
