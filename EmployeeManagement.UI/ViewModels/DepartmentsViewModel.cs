﻿using EmployeeManagement.UI.Annotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.ViewModels
{
    public class DepartmentsViewModel : INotifyPropertyChanged
    {
        private EmployeeDetailsViewModel _employeeDetailsViewModel;
        public EmployeeDetailsViewModel EmployeeDetailsViewModel
        {
            get => _employeeDetailsViewModel;
            set
            {
                _employeeDetailsViewModel = value;
                OnPropertyChanged(nameof(EmployeeDetailsViewModel));
            }
        }

        private EmployeeListViewModel _employeeListViewModel;
        public EmployeeListViewModel EmployeeListViewModel
        {
            get => _employeeListViewModel;
            set
            {
                _employeeListViewModel = value;
                OnPropertyChanged(nameof(EmployeeListViewModel));
            }
        }

        public DepartmentsViewModel(EmployeeDetailsViewModel employeeDetailsViewModel, EmployeeListViewModel employeeListViewModel)
        {
            _employeeDetailsViewModel = employeeDetailsViewModel;
            _employeeListViewModel = employeeListViewModel;
        }

        public async Task InitAsync(Departments department)
        {
            _employeeListViewModel.AssignEmployeeHandler += _employeeDetailsViewModel.SetEmployeeHandler;
            _employeeDetailsViewModel.UpdateEmployeeHandler += _employeeListViewModel.UpdateCurrentEmployeeHandler;
            await _employeeDetailsViewModel.SetDepartments();
            await _employeeListViewModel.UpdateEmployees(department);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
