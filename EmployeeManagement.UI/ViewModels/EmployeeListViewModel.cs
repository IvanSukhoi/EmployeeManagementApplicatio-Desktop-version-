using System.Collections.Generic;
using System.Collections.ObjectModel;
using EmployeeManagement.UI.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Extensions;
using EmployeeManagement.UI.Settings.Localization;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

        private readonly IMapperWrapper _mapperWrapper;

        public delegate void AssignItemEventHandler(EmployeeViewModel employeeViewModel);
        public event AssignItemEventHandler AssignEmployeeHandler;

        public IDelegateCommand CreateEmployeeCommand { protected set; get; }

        public Departments CurrentDepartment { get; set; }

        public EmployeeListViewModel(EmployeeService employeeService, IMapperWrapper mapperWrapper, DepartmentService departmentService)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
            _departmentService = departmentService;
            CreateEmployeeCommand = new DelegateCommand.DelegateCommandAsync(ExecuteCreateEmployee);
        }

        private ObservableCollection<EmployeeViewModel> _employees;

        public ObservableCollection<EmployeeViewModel> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private EmployeeViewModel _currentEmployeeItem;
        public EmployeeViewModel CurrentEmployeeViewItem
        {
            get => _currentEmployeeItem;
            set
            {
                _currentEmployeeItem = value;
                OnAssignEmployee(CurrentEmployeeViewItem);
            }
        }

        public async Task ExecuteCreateEmployee(object parameter)
        {
            var department = await _departmentService.GetByDepartmentIdAsync((int)CurrentDepartment);
            CurrentEmployeeViewItem = new EmployeeViewModel
            {
                IsNew = true,
                DepartmentId = (int)CurrentDepartment,
                DepartmentName = department.Name
            };
        }

        public async Task UpdateEmployees(Departments department)
        {
            CurrentDepartment = department;
            Employees = new ObservableCollection<EmployeeViewModel>();

            var listEmployees = _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(await _employeeService.GetByDepartmentIdAsync((int)department));
                
            listEmployees.ForEach(x => x.DepartmentName = Resource.ResourceManager.GetString(x.DepartmentName));

            Employees.AddRange(listEmployees);

            CurrentEmployeeViewItem = Employees.FirstOrDefault();
        }

        public void UpdateCurrentEmployeeHandler()
        {
            if ((!CurrentEmployeeViewItem.IsNew && CurrentEmployeeViewItem.IsEditedDepartment) || CurrentEmployeeViewItem.IsDeleted)
            {
                Employees.Remove(CurrentEmployeeViewItem);
                CurrentEmployeeViewItem = Employees.FirstOrDefault();
            }

            if (CurrentEmployeeViewItem != null && CurrentEmployeeViewItem.IsNew)
            {
                Employees.Add(CurrentEmployeeViewItem);
                CurrentEmployeeViewItem.IsNew = false;
            }

            OnPropertyChanged(nameof(Employees));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnAssignEmployee(EmployeeViewModel employeemodel)
        {
            AssignEmployeeHandler?.Invoke(employeemodel);
        }
    }
}
