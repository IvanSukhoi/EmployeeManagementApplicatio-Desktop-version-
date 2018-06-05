using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService 
    {
        private readonly ManagementContext _managementContext;

        public EmployeeService(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        public List<Employee> GetByDepartment(Departments department)
        {
            return _managementContext.Employees.Where(x => x.DepartmentID == (int) department).ToList();
        }
    }
}
