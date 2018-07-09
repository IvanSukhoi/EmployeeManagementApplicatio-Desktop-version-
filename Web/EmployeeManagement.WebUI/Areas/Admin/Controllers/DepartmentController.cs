using AutoMapper;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.WebUI.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        readonly IDepartmentService _departmentService;
        readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public ViewResult Index()
        {
            var result = _departmentService.GetAll().Select(x => _mapper.Map<DepartmentModel, DepartmentModel>(x)).ToList();

            return View(result);
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            var departmentModel = _mapper.Map<DepartmentModel, DepartmentModel>(_departmentService.GetById(id));

            return new JsonResult { Data = departmentModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult Edit(DepartmentViewModel departmentModel)
        {
            if (!string.IsNullOrEmpty(departmentModel.Name))
            {
                var department = _mapper.Map<DepartmentViewModel, DepartmentModel>(departmentModel);

                if (departmentModel.Id == 0)
                {
                    _departmentService.Create(department);
                }
                else
                {
                    _departmentService.Save(department);
                }

                TempData["message"] = $"{department.Name} department has been saved";

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            return View("Edit", new DepartmentViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            _departmentService.Delete(id);
            return department != null ? new JsonResult { Data =  new { message = $"{department.Name} department was deleted"} } : new JsonResult { Data = new { message = "Department was not found" } };
        }
    }
}