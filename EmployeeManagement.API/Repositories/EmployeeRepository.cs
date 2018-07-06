using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.API.Helpers;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class EmployeeRepository
    {
        private readonly WebClient _webClient;

        public EmployeeRepository(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<List<EmployeeModel>> GetByDepartmentIdAsync(int departmentId)
        {
            var response = await _webClient.GetAsync("Employee/GetByDepartmentId/" + departmentId);

            return await (response?.Content).ReadAsAsync<List<EmployeeModel>>();
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync("Employee/GetById/" + id);

            return await (response?.Content).ReadAsAsync<EmployeeModel>();
        }

        public async Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel)
        {
            var response = await _webClient.PostAsync("Employee/Create", employeeModel);

            return await (response?.Content).ReadAsAsync<EmployeeModel>();
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
