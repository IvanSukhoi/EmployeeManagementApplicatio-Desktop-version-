using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;

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

        public List<DepartmentModel> GetAllStatistics()
        {
            //TODO rename departments
            return _managementContext.Departments.Select(x =>
                new DepartmentModel {Department = x, QuantityEmployees = x.Employees.Count}).ToList();
        }
    }
}
