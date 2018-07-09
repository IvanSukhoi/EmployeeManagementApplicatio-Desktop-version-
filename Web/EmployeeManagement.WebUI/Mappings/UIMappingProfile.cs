using AutoMapper;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.WebUI.Enums;
using EmployeeManagement.WebUI.Models;

namespace EmployeeManagement.WebUI.Mappings
{
    public class UiMappingProfile : Profile
    {
        public UiMappingProfile()
        {
            CreateMap<DepartmentModel, DepartmentViewModel>()
                .ReverseMap();
            CreateMap<EmployeeModel, EmployeeViewModel>()
                .ReverseMap();

            CreateMap<EmployeeViewModel, EmployeeViewModel>();
            CreateMap<DepartmentModel, DepartmentModel>();
            CreateMap<EmployeeModel, EmployeeModel>();
        }
    }
}