using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService 
    {
        private readonly ManagementContext _managementContext;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public List<EmployeeModel> GetByDepartment(Departments department)
        {
            var data = _managementContext.Employees.Include("Department").Where(x => x.DepartmentID == (int) department).ToList();
            var employeeModels = _mapperWrapper.Map<List<Employee>, List<EmployeeModel>>(data);

            return employeeModels;
        }

        public EmployeeModel Create(EmployeeModel employeeModel)
        {
            var employee = _mapperWrapper.Map<EmployeeModel, Employee>(employeeModel);
            employee.Department = _managementContext.Departments.FirstOrDefault(x => x.ID == employee.DepartmentID);

            _managementContext.Employees.Add(employee);
            _managementContext.SaveChanges();

            return _mapperWrapper.Map<Employee, EmployeeModel>(_managementContext.Employees.AsEnumerable().Last());
        }

        public void Save(EmployeeModel employeeModel)
        {
            var dbEntry = _managementContext.Employees.FirstOrDefault(x => x.ID == employeeModel.Id);

            if (dbEntry == null) return;

            _mapperWrapper.Map(employeeModel, dbEntry);

            dbEntry.DepartmentID = dbEntry.DepartmentID;
            _managementContext.Entry(dbEntry).Reference(x => x.Department).Load();
            _managementContext.SaveChanges();
        }
    }
}
