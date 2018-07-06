using System.ComponentModel;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Managers;
using System.Deployment.Application;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contacts.Enums;
using EmployeeManagement.UI.Annotations;

namespace EmployeeManagement.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public IDelegateCommand SelectByDepartmentCommand { protected set; get; }
        public IDelegateCommand ChangeSettingsCommand { protected set; get; }

        private readonly NavigationManager _navigationManager;

        private Departments _currentDepartment;
        public Departments CurrentDepartment
        {
            get => _currentDepartment;
            set
            {
                _currentDepartment = value;
                OnPropertyChanged(nameof(CurrentDepartment));
            }
        }

        private Contacts.Enums.Pages _currentPage;
        public Contacts.Enums.Pages CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public string Version { get; set; }

        public  MainViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            SelectByDepartmentCommand = new DelegateCommandAsync(ExecuteSelectByDepartmentAsync);
            ChangeSettingsCommand = new DelegateCommandAsync(ExecuteChangeSettingsAsync);
        }

        public async Task InitAsync()
        {
            await _navigationManager.Navigate(Contacts.Enums.Pages.HomePage, Departments.NotSelected);

            Version = ApplicationDeployment.IsNetworkDeployed
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : "Development";
        }

        public async Task ExecuteSelectByDepartmentAsync(object parameter)
        {
            var values = (object[])parameter;

            CurrentPage = (Contacts.Enums.Pages)values[0];
            CurrentDepartment = (Departments)values[1];

            await _navigationManager.Navigate(CurrentPage, CurrentDepartment);
        }

        public async Task ExecuteChangeSettingsAsync(object parameter)
        {
            CurrentDepartment = Departments.NotSelected;
            CurrentPage = (Contacts.Enums.Pages)parameter;

            await _navigationManager.Navigate(CurrentPage, Departments.NotSelected);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
