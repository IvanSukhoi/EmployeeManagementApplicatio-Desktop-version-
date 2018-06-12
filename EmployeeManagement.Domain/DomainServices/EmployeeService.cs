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
            var employees = _managementContext.Employees.Where(x => x.DepartmentID == (int) department).ToList();

            return employees;
        }

        public Employee Create(Employee employee)
        {
            employee.Department =
                _managementContext.Departments.FirstOrDefault(x => x.ID == employee.Department.ID);
            _managementContext.Employees.Add(employee);
            _managementContext.SaveChanges();

            return _managementContext.Employees.AsEnumerable().Last();
        }

        public void Save(Employee employee)
        {
            var dbEntry = _managementContext.Employees.FirstOrDefault(x => x.ID == employee.ID);

            if (dbEntry == null) return;

            dbEntry.FirstName = employee.FirstName;
            dbEntry.MidleName = employee.MidleName;
            dbEntry.LastName = employee.LastName;
            dbEntry.Sex = employee.Sex;
            dbEntry.Position = employee.Position;
            dbEntry.Profession = employee.Profession;
            dbEntry.DepartmentID = employee.Department.ID;
            _managementContext.Entry(dbEntry).Reference(x => x.Department).Load();
            _managementContext.SaveChanges();
        }
    }
}
