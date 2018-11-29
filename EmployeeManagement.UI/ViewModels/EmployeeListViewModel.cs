using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Events;
using EmployeeManagement.UI.UiInterfaces.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeListViewModel: BindableBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IResourceManagerService _resourceManagerService;

        private readonly IMapperWrapper _mapperWrapper;

        private readonly IEventAggregator _eventAggregator;

        private ICollectionView _employees;

        public ICollectionView Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }

        private ObservableCollection<EmployeeViewModel> _employeeViewModels; 

        public ICommand CreateEmployeeCommand { protected set; get; }

        public EmployeeViewModel EmployeeViewModel { get; set; }

        public Departments CurrentDepartment { get; set; }


        public EmployeeListViewModel(IEmployeeService employeeService, IMapperWrapper mapperWrapper, IDepartmentService departmentService, IResourceManagerService resourceManagerService, IEventAggregator eventAggregator)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
            _departmentService = departmentService;
            _resourceManagerService = resourceManagerService;
            _eventAggregator = eventAggregator;
            CreateEmployeeCommand = new DelegateCommand(async () => await ExecuteCreateEmployeeAsync());
        }

        public void SubscribeToTheEvent()
        {
            _eventAggregator.GetEvent<SaveEmployeeViewModelEvent>().Subscribe(UpdateCurrentEmployeeHandler);
        }

        public async Task ExecuteCreateEmployeeAsync()
        {
            var department = await _departmentService.GetByDepartmentIdAsync((int)CurrentDepartment);
            EmployeeViewModel = new EmployeeViewModel
            {
                IsNew = true,
                DepartmentId = (int)CurrentDepartment,
                DepartmentName = department.Name
            };

            _eventAggregator.GetEvent<UpdateEmployeeViewModelEvent>().Publish(EmployeeViewModel);
        }

        public async Task UpdateEmployees(Departments department)
        {
            _employeeViewModels = new ObservableCollection<EmployeeViewModel>();

            CurrentDepartment = department;

            var listEmployees = _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(await _employeeService.GetByDepartmentIdAsync((int)department));
            listEmployees.ForEach(x => x.DepartmentName = _resourceManagerService.GetString(x.DepartmentName));

            _employeeViewModels.AddRange(listEmployees);

            Employees = new ListCollectionView(_employeeViewModels);
            Employees.CurrentChanged += SelectedItemChanged;

            Employees.MoveCurrentToFirst();
            EmployeeViewModel = Employees.CurrentItem as EmployeeViewModel;
            _eventAggregator.GetEvent<UpdateEmployeeViewModelEvent>().Publish(EmployeeViewModel);
        }

        public void UpdateCurrentEmployeeHandler()
        {
            if ((!EmployeeViewModel.IsNew && EmployeeViewModel.IsEditedDepartment) || EmployeeViewModel.IsDeleted)
            {
                _employeeViewModels.Remove(EmployeeViewModel);
                EmployeeViewModel = _employeeViewModels.FirstOrDefault();
            }

            if (EmployeeViewModel != null && EmployeeViewModel.IsNew)
            {
                _employeeViewModels.Add(EmployeeViewModel);
                EmployeeViewModel.IsNew = false;
            }

            RaisePropertyChanged(nameof(Employees));
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            EmployeeViewModel = Employees.CurrentItem as EmployeeViewModel;
            _eventAggregator.GetEvent<UpdateEmployeeViewModelEvent>().Publish(EmployeeViewModel);
        }
    }
}
