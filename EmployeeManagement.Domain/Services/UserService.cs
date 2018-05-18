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

        public User Check(string Login, string Password)
        {
            return _managementContext.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
        }
    }
}
