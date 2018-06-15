﻿using System.Collections.ObjectModel;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Extensions;
using EmployeeManagement.UI.Mappings;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private readonly ModelViewFactory _modelViewFactory;

        public delegate void AssignItemEventHandler(EmployeeViewModel employeeViewModel);
        public event AssignItemEventHandler AssignEmployeeHandler;

        public IDelegateCommand CreateEmployeeCommand { protected set; get; }

        public EmployeeListViewModel(EmployeeService employeeService, ModelViewFactory modelViewFactory)
        {
            _employeeService = employeeService;
            _modelViewFactory = modelViewFactory;
            CreateEmployeeCommand = new DelegateCommand.DelegateCommand(ExecuteCreateEmployee);
        }

        public ObservableCollection<EmployeeViewModel> Employees { get; set; }

        private EmployeeViewModel _currentEmployeeItem;
        public EmployeeViewModel CurrentEmployeeViewItem
        {
            get => _currentEmployeeItem;
            set
            {
                _currentEmployeeItem = value;
                OnAssignEmployee(_currentEmployeeItem);
            }
        }

        public void ExecuteCreateEmployee(object parameter)
        {
            CurrentEmployeeViewItem = new EmployeeViewModel { IsNew = true };
        }

        public void UpdateEmployees(Departments department)
        {
            Employees = new ObservableCollection<EmployeeViewModel>();

            var listEmployees = _employeeService.GetByDepartment(department)
                .Select(x => _modelViewFactory.MappToEmployeeViewModel(x)).ToList();

            Employees.AddRange(listEmployees);

            CurrentEmployeeViewItem = Employees.FirstOrDefault();
        }

        public void UpdateCurrentEmployeeHandler()
        {
            if (!CurrentEmployeeViewItem.IsNew && CurrentEmployeeViewItem.IsEditedDepartment)
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
