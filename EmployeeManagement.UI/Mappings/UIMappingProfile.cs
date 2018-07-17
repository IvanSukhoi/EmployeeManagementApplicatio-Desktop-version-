using AutoMapper;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Mappings
{
    public class UiMappingProfile : Profile
    {
        public UiMappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeViewModel>()
                .ForMember(c => c.IsNew, opt => opt.Ignore())
                .ForMember(c => c.IsDeleted, opt => opt.Ignore())
                .ForMember(c => c.IsEditedDepartment, opt => opt.Ignore());

            CreateMap<EmployeeViewModel, EmployeeModel>();

            CreateMap<EmployeeViewModel, EmployeeViewModel>();

            CreateMap<DepartmentModel, DepartmentModel>();
            CreateMap<EmployeeModel, EmployeeModel>();
        }
    }
}
