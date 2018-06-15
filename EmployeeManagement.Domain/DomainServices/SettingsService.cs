using System.Linq;
using AutoMapper;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.Domain.DomainServices
{
    public class SettingsService
    {
        private readonly ManagementContext _managementContext;
        private readonly IMapper _mapper;

        public SettingsService(ManagementContext managementContext, IMapper mapper)
        {
            _managementContext = managementContext;
            _mapper = mapper;
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
                _mapper.Map(settings, dbEntry);
                _managementContext.Entry(dbEntry).Reference(x => x.User).Load();
            }

            _managementContext.SaveChanges();
        }
    }
}
