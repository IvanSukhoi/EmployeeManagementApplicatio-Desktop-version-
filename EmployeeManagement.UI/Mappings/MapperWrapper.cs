using AutoMapper;
using EmployeeManagement.Domain.Mappings;

namespace EmployeeManagement.UI.Mappings
{
    public class MapperWrapper: IMapperWrapper
    {
        public MapperWrapper()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile<DomainMappingProfile>();
                c.AddProfile<UIMappingProfile>();
            });
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }
    }
}