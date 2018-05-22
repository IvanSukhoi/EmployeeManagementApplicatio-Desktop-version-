using System.Diagnostics.Eventing.Reader;
using System.Linq;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.DataEF;

namespace EmployeeManagement.Domain.Services
{
    public class UserService
    {
        private readonly ManagementContext  _managementContext;

        public UserService(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        public User GetUser(string login, string password)
        {
            return _managementContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
        }
    }
}
