using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Extensions;
using EmployeeManagement.UI.Settings.Localization;
using Unity.Interception.Utilities;

namespace EmployeeManagement.UI.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<DepartmentModel> Departments { get; set; }

        private readonly IDepartmentService _departmentService;
        
        public HomeViewModel(IDepartmentService departmentService)
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
