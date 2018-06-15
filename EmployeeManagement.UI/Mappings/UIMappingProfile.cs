using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Mappings
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.DepartmentId, opt => opt.MapFrom(c => c.DepartmentID))
                .ForMember(s => s.ManagerId, opt => opt.MapFrom(c => c.ManagerID))
                .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName));

            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(s => s.ID, opt => opt.MapFrom(c => c.Id))
                .ForMember(s => s.DepartmentID, opt => opt.MapFrom(c => c.DepartmentId))
                .ForMember(s => s.ManagerID, opt => opt.MapFrom(c => c.ManagerId))
                .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName));

            CreateMap<EmployeeViewModel, EmployeeViewModel>();
            CreateMap<Department, Department>();

            CreateMap<Employee, Employee>();
            CreateMap<Department, Department>();
        }
    }
}
