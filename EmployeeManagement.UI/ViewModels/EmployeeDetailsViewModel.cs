using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Mappings;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeDetailsViewModel : INotifyPropertyChanged
    {
        private EmployeeViewModel _currentEmployeeView;

        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;
        private readonly ModelViewFactory _modelViewFactory;

        public delegate void UpdateItemEventHandler();
        public event UpdateItemEventHandler UpdateEmployeeHandler;

        public IDelegateCommand EditCommand { protected set; get; }
        public IDelegateCommand CancelCommand { protected set; get; }
        public IDelegateCommand SaveCommand { protected set; get; }

        public List<Department> Departments { get; set; }
        public List<Sex> SexTypes => Enum.GetValues(typeof(Sex)).Cast<Sex>().ToList();
        public List<Profession> ProfessionTypes => Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();

        public EmployeeDetailsViewModel(EmployeeService employeeService, ModelViewFactory modelViewFactory, DepartmentService departmentService)
        {
            _employeeService = employeeService;
            _modelViewFactory = modelViewFactory;
            _departmentService = departmentService;
            EditCommand = new DelegateCommand.DelegateCommand(ExecuteEditCommand);
            CancelCommand = new DelegateCommand.DelegateCommand(ExecuteCancelCommand);
            SaveCommand = new DelegateCommand.DelegateCommand(ExecuteSaveCommand);
        }

        private bool _isEditingEmployee;

        public bool IsEditingEmployee
        {
            get => _isEditingEmployee;
            set
            {
                _isEditingEmployee = value;
                OnPropertyChanged(nameof(IsEditingEmployee));
            }
        }

        private EmployeeViewModel _employeeViewModel;

        public EmployeeViewModel EmployeeViewModel
        {
            get => _employeeViewModel;
            set
            {
                _employeeViewModel = value;
                OnPropertyChanged(nameof(EmployeeViewModel));
            }
        }

        public void SetDepartments()
        {
            Departments = _departmentService.GetAll();
        }

        public void ExecuteEditCommand(object parameter)
        {
            IsEditingEmployee = true;
        }

        public void ExecuteCancelCommand(object parameter)
        {
            AssignEmployeeModel();
            IsEditingEmployee = false;
        }

        public void ExecuteSaveCommand(object parameter)
        {
            if (_employeeViewModel.IsNew)
            {
                var employee = _employeeService.Create(_modelViewFactory.MappToEmployee(EmployeeViewModel));
                _modelViewFactory.CloneEmployeeToEmployeeViewModel(employee, EmployeeViewModel);
            }
            else
            {
                _employeeService.Save(_modelViewFactory.MappToEmployee(EmployeeViewModel));

                if (_employeeViewModel.Department.ID != _currentEmployeeView.Department.ID)
                {
                    _employeeViewModel.IsEditedDepartment = true;
                }
            }

            IsEditingEmployee = false;
            _modelViewFactory.CloneEmployeeViewModel(EmployeeViewModel, _currentEmployeeView);
            OnUpdateEmployee();
        }

        public void SetEmployeeHandler(EmployeeViewModel employee)
        {
            _currentEmployeeView = employee;
            AssignEmployeeModel();

            if (employee != null)
            {
                IsEditingEmployee = employee.IsNew;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnUpdateEmployee()
        {
            UpdateEmployeeHandler?.Invoke();
        }

        public void AssignEmployeeModel()
        {
            if (_currentEmployeeView == null) return;

            EmployeeViewModel = _modelViewFactory.CloneEmployeeViewModel(_currentEmployeeView);
        }
    }
}
