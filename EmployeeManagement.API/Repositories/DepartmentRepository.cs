using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class DepartmentRepository
    {
        private readonly IWebClient _webClient;

        public DepartmentRepository(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            return await _webClient.GetAsync<List<DepartmentModel>>("Department/GetAll");
        }

        public async Task<DepartmentModel> GetByIdAsync(int id)
        {
            return await _webClient.GetAsync<DepartmentModel>("Department/GetById/" + id);
        }
    }
}
