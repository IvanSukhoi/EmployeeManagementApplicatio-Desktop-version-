using System.Linq;
using EmployeeManagement.DataEF;
using EmployeeManagement.DataEF.DAL;

namespace EmployeeManagement.Domain.DomainServices
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
