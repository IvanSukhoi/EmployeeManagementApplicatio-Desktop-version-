using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.API.Helpers;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.API.Repositories
{
    public class DepartmentRepository
    {
        private readonly WebClient _webClient;

        public DepartmentRepository(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<List<DepartmentModel>> GetAll()
        {
            var response = await _webClient.GetAsync("Department/GetAll");

            return await (response?.Content).ReadAsAsync<List<DepartmentModel>>();
        }

        public async Task<DepartmentModel> GetById(int id)
        {
            var response = await _webClient.GetAsync("Department/GetById/" + id);

            return await (response?.Content).ReadAsAsync<DepartmentModel>();
        }
    }
}
