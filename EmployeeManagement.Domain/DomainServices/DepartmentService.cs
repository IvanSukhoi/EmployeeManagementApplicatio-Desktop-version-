using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentModel> GetByDepartmentIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }
    }
}
