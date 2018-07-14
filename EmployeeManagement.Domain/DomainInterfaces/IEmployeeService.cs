using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetByDepartmentIdAsync(int id);
        Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel);
        Task SaveAsync(EmployeeModel employeeModel);
        Task DeleteAsync(int id);
    }
}
