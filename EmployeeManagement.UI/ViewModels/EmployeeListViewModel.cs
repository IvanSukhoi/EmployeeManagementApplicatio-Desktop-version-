using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeManagement.DataEF;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.UI.DelegateCommand;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }

        public List<Sex> SexTypes => Enum.GetValues(typeof(Sex)).Cast<Sex>().ToList();
        public List<Profession> ProfessionTypes => Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();

        public IDelegateCommand<object> EditCommand { protected set; get; }

        public EmployeeListViewModel(EmployeeService employeeService, DepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            EditCommand = new DelegateCommand<object>(ExecuteEditCommand);
        }

        public bool IsEditingEmployee { get; set; }

        public void ExecuteEditCommand(object parametr)
        {
            IsEditingEmployee = (bool)parametr;
            OnPropertyChanged(nameof(IsEditingEmployee));
        }

        public void SetEmployees(Departments department)
        {
            Employees = _employeeService.GetByDepartment(department);
            Departments = _departmentService.GetAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
