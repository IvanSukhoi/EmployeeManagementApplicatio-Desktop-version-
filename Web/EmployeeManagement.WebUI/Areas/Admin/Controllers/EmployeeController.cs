using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.WebUI.Enums;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapperWrapper _mapperWrapper;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
        }

        //public ViewResult Index()
        //{
        //    var employees = _employeeService.GetAll();

        //    return View(employees.Select(x => _mapperFactory.MappEmployeeToEmployeeModel<EmployeeViewModel>(x)).ToList());
        //}

        //[HttpGet]
        //public JsonResult EditManager(int id)
        //{
        //    var managerModel = _mapperFactory.MappEmployeeToEmployeeModel<Models.ManagerViewModel>(_employeeService.GetById(id));
        //    BuildEmployeeModel(managerModel);

        //    return new JsonResult { Data = managerModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //[HttpGet]
        //public JsonResult EditDeveloper(int id)
        //{
        //    var developerModel = _mapperFactory.MappEmployeeToEmployeeModel<Models.DeveloperViewModel>(_employeeService.GetById(id));
        //    BuildEmployeeModel(developerModel);

        //    return new JsonResult { Data = developerModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //[HttpGet]
        //public JsonResult EditServiceWorker(int id)
        //{
        //    var serviceWorkerModel = _mapperFactory.MappEmployeeToEmployeeModel<Models.ServiceWorkerViewModel>(_employeeService.GetById(id));
        //    BuildEmployeeModel(serviceWorkerModel);

        //    return new JsonResult { Data = serviceWorkerModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


        //public JsonResult CreateDeveloper()
        //{
        //    var departmentModels = _departmentService.GetAll().Select(x => _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(x)).ToList();

        //    return new JsonResult { Data = new DeveloperViewModel { DepartmentModelList = departmentModels }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //public ViewResult CreateManager()
        //{
        //    var departmentModels = _departmentService.GetAll().Select(x => _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(x)).ToList();

        //    return View("Edit", new ManagerViewModel { DepartmentModelList = departmentModels, ActionMethod = ActionMethod.Create});
        //}

        //public ViewResult CreateServiceWorker()
        //{
        //    var departmentModels = _departmentService.GetAll().Select(x => _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(x)).ToList();

        //    return View("Edit", new ServiceWorkerViewModel { DepartmentModelList = departmentModels, ActionMethod = ActionMethod.Create});
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var employee = _employeeService.GetById(id);
        //    _employeeService.Delete(id);
        //    if (employee != null)
        //    {
        //        TempData["message"] = $"{employee.FirstName + employee.LastName} was deleted";
        //    }
        //    return RedirectToAction("Index");
        //}

        //private void BuildEmployeeModel(EmployeeViewModel employeeModel)
        //{
        //    employeeModel.ActionMethod = ActionMethod.Edit;
        //    employeeModel.DepartmentModelList = _departmentService.GetAll().Select(x => _mapperWrapper.Map<DepartmentModel, DepartmentViewModel>(x)).ToList();
        //}
    }
}