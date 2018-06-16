using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.Domain.Mappings;

namespace EmployeeManagement.Domain.DomainServices
{
    public class SettingsService
    {
        private readonly ManagementContext _managementContext;

        private readonly IMapperWrapper _mapperWrapper;

        public SettingsService(ManagementContext managementContext, IMapperWrapper mapperWrapper)
        {
            _managementContext = managementContext;
            _mapperWrapper = mapperWrapper;
        }

        public Settings GetById(int id)
        {
            return _managementContext.Settings.FirstOrDefault(x => x.UserID == id);
        }

        public void Save(Settings settings)
        {
            var dbEntry = _managementContext.Settings.FirstOrDefault(x => x.UserID == settings.UserID);

            if (dbEntry == null)
            {
                _managementContext.Settings.Add(settings);
                _managementContext.SaveChanges();
            }
            else
            {
                _mapperWrapper.Map(settings, dbEntry);
                _managementContext.Entry(dbEntry).Reference(x => x.User).Load();
            }

            _managementContext.SaveChanges();
        }
    }
}
