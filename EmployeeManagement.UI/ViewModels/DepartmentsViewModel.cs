using EmployeeManagement.UI.Annotations;
using EmployeeManagement.Domain.Enums;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using EmployeeManagement.DataEF.Entities;

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

        public void Init(Departments department)
        {
            _employeeListViewModel.AssignEmployeeHandler += _employeeDetailsViewModel.SetEmployeeHandler;
            _employeeDetailsViewModel.UpdateEmployeeHandler += _employeeListViewModel.UpdateCurrentEmployeeHandler;
            _employeeDetailsViewModel.SetDepartments();
            _employeeListViewModel.UpdateEmployees(department);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
