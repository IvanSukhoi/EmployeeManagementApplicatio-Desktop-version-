using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Mappings
{
    public class ModelViewFactory
    {
        private readonly IMapper _mapper;

        public ModelViewFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public EmployeeViewModel MappToEmployeeViewModel(Employee employee)
        {
            return _mapper.Map<Employee, EmployeeViewModel>(employee);
        }

        public Employee MappToEmployee(EmployeeViewModel employeeViewModel)
        {
            return _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
        }

        public void CloneEmployeeViewModel(EmployeeViewModel original, EmployeeViewModel copy)
        {
            _mapper.Map(original, copy);
        }

        public void CloneEmployeeToEmployeeViewModel(Employee employee, EmployeeViewModel employeeViewModel)
        {
            _mapper.Map(employee, employeeViewModel);
        }
    }
}
