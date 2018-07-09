using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapperWrapper _mapperWrapper;
        public int PageSize = 4;

        public EmployeeController(IEmployeeService employeeService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _mapperWrapper = mapperWrapper;
        }

        public ViewResult List(string category, int managerId, int page = 1)
        {
            var employeeModels = managerId == 0 ? _employeeService.GetAll().Select(x => 
           _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(x)).ToList() : GetTreeEmployeeList(managerId);

            TempData["ManagerId"] = managerId;

            var model = new EmployeeListViewModel
            {
                ManagerId = managerId,

                Employees = employeeModels
                .Where(x => category == null || x.DepartmentName == category)
                .OrderBy(x => x.FirstName)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? employeeModels.Count() :
                        employeeModels.Count(e => e.DepartmentName == category)
                },

                CurrentCategory = category
            };

            return View(model);
        }

        public List<EmployeeViewModel> GetTreeEmployeeList(int managerId)
        {
            var managerModel = _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(managerId));

            var employeeModels =
                _mapperWrapper.Map<List<EmployeeModel>, List<EmployeeViewModel>>(
                    _employeeService.GetByManagerId(managerId));

            TempData["Manager"] = $"{managerModel.FirstName + managerModel.LastName}";

            return employeeModels;
        }

        public ViewResult GetManagerEmployee(int employeeId)
        {
            var employeeModel = _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById(employeeId));

            var managerModel = _mapperWrapper.Map<EmployeeModel, EmployeeViewModel>(_employeeService.GetById((int)employeeModel.ManagerId));

            TempData["Employee"] = $"{employeeModel.FirstName + employeeModel.LastName}";

            return View(managerModel);
        }
    }
}
