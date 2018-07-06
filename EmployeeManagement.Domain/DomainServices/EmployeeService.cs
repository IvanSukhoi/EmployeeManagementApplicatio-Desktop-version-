using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeModel>> GetByDepartmentIdAsync(int id)
        {
            return await _employeeRepository.GetByDepartmentIdAsync(id);
        }

        public async Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel)
        {
            return await _employeeRepository.CreateAsync(employeeModel);
        }

        public async Task SaveAsync(EmployeeModel employeeModel)
        {
            await _employeeRepository.SaveAsync(employeeModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
