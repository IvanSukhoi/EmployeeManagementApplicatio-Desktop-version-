using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using Prism.Mvvm;
using Prism.Regions;

namespace EmployeeManagement.UI.ViewModels
{
    public class DepartmentsViewModel : BindableBase, IRegionMemberLifetime
    {

        private EmployeeDetailsViewModel _employeeDetailsViewModel;
        public EmployeeDetailsViewModel EmployeeDetailsViewModel
        {
            get => _employeeDetailsViewModel;
            set
            {
                _employeeDetailsViewModel = value;
                RaisePropertyChanged(nameof(EmployeeDetailsViewModel));
            }
        }

        private EmployeeListViewModel _employeeListViewModel;
        public EmployeeListViewModel EmployeeListViewModel
        {
            get => _employeeListViewModel;
            set
            {
                _employeeListViewModel = value;
                RaisePropertyChanged(nameof(EmployeeListViewModel));
            }
        }

        public DepartmentsViewModel(EmployeeDetailsViewModel employeeDetailsViewModel, EmployeeListViewModel employeeListViewModel)
        {
            _employeeDetailsViewModel = employeeDetailsViewModel;
            _employeeListViewModel = employeeListViewModel;
        }

        public async Task InitAsync(Departments department)
        {
            _employeeListViewModel.SubscribeToTheEvent();
            _employeeDetailsViewModel.SubscribeToTheEvent();
            await _employeeListViewModel.UpdateEmployees(department);
        }

        public bool KeepAlive => false;
    }
}
