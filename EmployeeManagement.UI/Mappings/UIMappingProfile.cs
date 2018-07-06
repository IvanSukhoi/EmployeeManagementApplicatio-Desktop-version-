using AutoMapper;
using EmployeeManagement.Contacts.Models;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Mappings
{
    public class UiMappingProfile : Profile
    {
        public UiMappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, EmployeeModel>();

            CreateMap<EmployeeViewModel, EmployeeViewModel>();

            CreateMap<DepartmentModel, DepartmentModel>();
            CreateMap<EmployeeModel, EmployeeModel>();
        }
    }
}
