using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EmployeeManagement.Contacts.Models;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Extensions;
using EmployeeManagement.UI.Settings.Localization;
using Unity.Interception.Utilities;

namespace EmployeeManagement.UI.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<DepartmentModel> Departments { get; set; }

        private readonly DepartmentService _departmentService;
        
        public HomeViewModel(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task InitAsync()
        {
            Departments = new ObservableCollection<DepartmentModel>();
            Departments.AddRange(await _departmentService.GetAllAsync());
            Departments.ForEach(x => x.Name = Resource.ResourceManager.GetString(x.Name));
        }
    }
}
