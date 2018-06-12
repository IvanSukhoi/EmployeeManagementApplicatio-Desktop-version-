using EmployeeManagement.Domain.Enums;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Domain.Managers;

namespace EmployeeManagement.UI.ViewModels
{
    public class MainViewModel
    {
        private readonly NavigationManager _navigationManager;

        public IDelegateCommand SelectByDepartmentCommand { protected set; get; }

        public MainViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            SelectByDepartmentCommand = new DelegateCommand<object>(ExecuteSelectByDepartment);
        }

        void ExecuteSelectByDepartment(object parametr)
        {
            var values = (object[])parametr;

            var page = (Pages.Pages)values[0];
            var department = (Departments) values[1];

            _navigationManager.Navigate(page, department);
        }
    }
}
