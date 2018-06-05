using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.DAL;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService
    {
        private readonly ManagementContext _managementContext;

        public DepartmentService(ManagementContext managementContext)
        {
            _managementContext = managementContext;
            
        }

        public List<Department> GetAll()
        {
            return _managementContext.Departments.ToList();
        }
    }
}
