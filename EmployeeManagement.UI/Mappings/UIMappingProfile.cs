using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Mappings
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, EmployeeModel>();

            CreateMap<EmployeeViewModel, EmployeeViewModel>();

            CreateMap<DepartmentModel, DepartmentModel>();
            CreateMap<EmployeeModel, EmployeeModel>();
        }
    }
}
