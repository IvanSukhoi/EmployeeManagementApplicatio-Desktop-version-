using System.Collections.Generic;
using System.Collections.ObjectModel;
using EmployeeManagement.UI.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Extensions;
using EmployeeManagement.UI.UiInterfaces;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IResourceManagerService _resourceManagerService;

        private readonly IMapperWrapper _mapperWrapper;

        public delegate void AssignItemEventHandler(EmployeeViewModel employeeViewModel);
        public event AssignItemEventHandler AssignEmployeeHandler;

        public IDelegateCommand CreateEmployeeCommand { protected set; get; }

        public Departments CurrentDepartment { get; set; }

        public EmployeeListViewModel(IEmployeeService employeeService, IMapperWrapper mapperWrapper, IDepartmentService departmentService, IResourceManagerService resourceManagerService)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
            _departmentService = departmentService;
            _resourceManagerService = resourceManagerService;
            CreateEmployeeCommand = new DelegateCommandAsync(ExecuteCreateEmployee);
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

        private EmployeeViewModel _currentEmployeeModel;
        public EmployeeViewModel CurrentEmployeeViewModel
        {
            get => _currentEmployeeModel;
            set
            {
                _currentEmployeeModel = value;
                OnAssignEmployee(CurrentEmployeeViewModel);
            }
        }

        public async Task ExecuteCreateEmployee(object parameter)
        {
            var department = await _departmentService.GetByDepartmentIdAsync((int)CurrentDepartment);
            CurrentEmployeeViewModel = new EmployeeViewModel
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
                
            listEmployees.ForEach(x => x.DepartmentName = _resourceManagerService.GetString(x.DepartmentName));

            Employees.AddRange(listEmployees);

            CurrentEmployeeViewModel = Employees.FirstOrDefault();
        }

        public void UpdateCurrentEmployeeHandler()
        {
            if ((!CurrentEmployeeViewModel.IsNew && CurrentEmployeeViewModel.IsEditedDepartment) || CurrentEmployeeViewModel.IsDeleted)
            {
                Employees.Remove(CurrentEmployeeViewModel);
                CurrentEmployeeViewModel = Employees.FirstOrDefault();
            }

            if (CurrentEmployeeViewModel != null && CurrentEmployeeViewModel.IsNew)
            {
                Employees.Add(CurrentEmployeeViewModel);
                CurrentEmployeeViewModel.IsNew = false;
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
