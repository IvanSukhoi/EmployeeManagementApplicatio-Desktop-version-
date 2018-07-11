using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetByDepartmentIdAsync(int id);
        Task<EmployeeModel> CreateAsync(EmployeeModel employeeModel);
        Task<EmployeeModel> GetByIdAsync(int id);
        Task SaveAsync(EmployeeModel employeeModel);
        Task DeleteAsync(int id);
    }
}
