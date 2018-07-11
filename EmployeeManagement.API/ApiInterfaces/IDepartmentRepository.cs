using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentModel>> GetAllAsync();
        Task<DepartmentModel> GetByIdAsync(int id);
    }
}
