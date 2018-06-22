using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Settings.Localization;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeDetailsViewModel : INotifyPropertyChanged
    {
        private EmployeeViewModel _currentEmployeeView;

        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

        public delegate void UpdateItemEventHandler();
        public event UpdateItemEventHandler UpdateEmployeeHandler;

        public IDelegateCommand EditCommand { protected set; get; }
        public IDelegateCommand CancelCommand { protected set; get; }
        public IDelegateCommand SaveCommand { protected set; get; }

        private readonly IMapperWrapper _mapperWrapper;

        public List<DepartmentModel> Departments { get; set; }
        public List<Sex> SexTypes => Enum.GetValues(typeof(Sex)).Cast<Sex>().ToList();
        public List<Profession> ProfessionTypes => Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();

        public EmployeeDetailsViewModel(EmployeeService employeeService, DepartmentService departmentService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
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
            Departments.ForEach(x => x.Name = Resource.ResourceManager.GetString(x.Name));
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
                var employee = _employeeService.Create(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(EmployeeViewModel));
                _mapperWrapper.Map(employee, EmployeeViewModel);
            }
            else
            {
                _employeeService.Save(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(EmployeeViewModel));

                if (_employeeViewModel.DepartmentId != _currentEmployeeView.DepartmentId)
                {
                    _employeeViewModel.IsEditedDepartment = true;
                }
            }

            IsEditingEmployee = false;
            _mapperWrapper.Map(EmployeeViewModel, _currentEmployeeView);
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

            EmployeeViewModel = _mapperWrapper.Map<EmployeeViewModel, EmployeeViewModel>(_currentEmployeeView);
        }
    }
}
