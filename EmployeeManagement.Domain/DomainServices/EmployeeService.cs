using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.DomainServices
{
    public class EmployeeService 
    {
        private readonly ManagementContext _managementContext;
        //TODO wrapper
        private readonly IMapper _mapper;

        public EmployeeService(ManagementContext managementContext, IMapper mapper)
        {
            _managementContext = managementContext;
            _mapper = mapper;
        }

        public List<Employee> GetByDepartment(Departments department)
        {
            var employees = _managementContext.Employees.Where(x => x.DepartmentID == (int) department).ToList();

            return employees;
        }

        public Employee Create(Employee employee)
        {
            employee.Department = _managementContext.Departments.FirstOrDefault(x => x.ID == employee.Department.ID);
            _managementContext.Employees.Add(employee);
            _managementContext.SaveChanges();

            return _managementContext.Employees.AsEnumerable().Last();
        }

        public void Save(Employee employee)
        {
            var dbEntry = _managementContext.Employees.FirstOrDefault(x => x.ID == employee.ID);

            if (dbEntry == null) return;

            _mapper.Map(employee, dbEntry);

            dbEntry.DepartmentID = employee.Department.ID;
            _managementContext.Entry(dbEntry).Reference(x => x.Department).Load();
            _managementContext.SaveChanges();
        }
    }
}
