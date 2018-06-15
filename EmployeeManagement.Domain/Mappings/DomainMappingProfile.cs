using AutoMapper;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.Domain.Mappings
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<Employee, Employee>();
            CreateMap<Department, Department>();
        }
    }
}