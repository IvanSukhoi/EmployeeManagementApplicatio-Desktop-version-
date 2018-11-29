using System.Deployment.Application;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.UI.UiInterfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace EmployeeManagement.UI.ViewModels
{
    public class MainViewModel: BindableBase
    {
        public ICommand SelectByDepartmentCommand { protected set; get; }
        public ICommand ChangeSettingsCommand { protected set; get; }

        private readonly INavigationManager _navigationManager;

        private Departments _currentDepartment;
        public Departments CurrentDepartment
        {
            get => _currentDepartment;
            set
            {
                _currentDepartment = value;
                RaisePropertyChanged(nameof(CurrentDepartment));
            }
        }

        private Contracts.Enums.Pages _currentPage;
        public Contracts.Enums.Pages CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                RaisePropertyChanged(nameof(CurrentPage));
            }
        }

        private string _version;

        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                RaisePropertyChanged(nameof(Version));
            }
        }

        public MainViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            SelectByDepartmentCommand = new DelegateCommand<object>(async (_) => await ExecuteSelectByDepartmentAsync(_));
            ChangeSettingsCommand = new DelegateCommand<object>(async (_) => await ExecuteChangeSettingsAsync(_));
        }

        public async Task InitAsync()
        {
            await _navigationManager.Navigate(Contracts.Enums.Pages.HomePage, Departments.NotSelected);

            Version = ApplicationDeployment.IsNetworkDeployed
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : "Development";
        }

        public async Task ExecuteSelectByDepartmentAsync(object parameter)
        {
            var values = (object[])parameter;

            CurrentPage = (Contracts.Enums.Pages)values[0];
            CurrentDepartment = (Departments)values[1];

            await _navigationManager.Navigate(CurrentPage, CurrentDepartment);
        }

        public async Task ExecuteChangeSettingsAsync(object parameter)
        {
            CurrentDepartment = Departments.NotSelected;
            CurrentPage = (Contracts.Enums.Pages)parameter;

            await _navigationManager.Navigate(CurrentPage, Departments.NotSelected);
        }
    }
}
