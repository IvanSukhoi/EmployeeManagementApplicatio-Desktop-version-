using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.Mappings
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.DepartmentId, opt => opt.MapFrom(c => c.DepartmentID))
                .ForMember(s => s.ManagerId, opt => opt.MapFrom(c => c.ManagerID))
                .ForMember(s => s.MiddleName, opt => opt.MapFrom(c => c.MidleName))
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(c => c.Department.Name));
                //.ReverseMap();

            CreateMap<EmployeeModel, Employee>()
                .ForMember(s => s.ID, opt => opt.MapFrom(c => c.Id))
                .ForMember(s => s.DepartmentID, opt => opt.MapFrom(c => c.DepartmentId))
                .ForMember(s => s.ManagerID, opt => opt.MapFrom(c => c.ManagerId))
                .ForMember(s => s.MidleName, opt => opt.MapFrom(c => c.MiddleName));

            CreateMap<Department, DepartmentModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(c => c.ID))
                .ForMember(s => s.QuantityEmployees, opt => opt.MapFrom(c => c.Employees.Count));

            CreateMap<Employee, Employee>();
            CreateMap<Department, Department>();
        }
    }
}