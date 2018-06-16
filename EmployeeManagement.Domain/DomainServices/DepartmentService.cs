using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class DepartmentService
    {
        private readonly ManagementContext _managementContext;

        private readonly IMapperWrapper _mapperWrapper;

        public DepartmentService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public List<DepartmentModel> GetAll()
        {
            return _managementContext.Departments.ToList().Select(x => _mapperWrapper.Map<Department, DepartmentModel>(x)).ToList();
        }

        public List<DepartmentModel> GetAllStatistics()
        {
            return _managementContext.Departments.Select(x =>
                new DepartmentModel {Name = x.Name, QuantityEmployees = x.Employees.Count}).ToList();
        }
    }
}
