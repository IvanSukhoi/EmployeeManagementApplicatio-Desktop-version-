using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.API.Repositories;
using EmployeeManagement.Contacts.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentModel> GetByDepartmentIdAsync(int id)
        {
            return await _departmentRepository.GetById(id);
        }

        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            return await _departmentRepository.GetAll();
        }
    }
}
