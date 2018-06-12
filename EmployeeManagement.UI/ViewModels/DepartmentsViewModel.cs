using EmployeeManagement.UI.Annotations;
using EmployeeManagement.Domain.Enums;
using System.Runtime.CompilerServices;
using System.ComponentModel;

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

        public void SetDepartment(Departments department)
        {
            _employeeListViewModel.AssignEmployee += _employeeDetailsViewModel.SetEmployee;
            _employeeDetailsViewModel.UpdateEmployee += _employeeListViewModel.UpdateCurrentEmployee;
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
