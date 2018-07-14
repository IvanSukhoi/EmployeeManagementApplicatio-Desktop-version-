using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentModel> GetByDepartmentIdAsync(int id);
        Task<List<DepartmentModel>> GetAllAsync();
    }
}
