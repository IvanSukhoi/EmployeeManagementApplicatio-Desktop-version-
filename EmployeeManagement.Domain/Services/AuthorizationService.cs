using EmployeeManagement.DataEF;

namespace EmployeeManagement.Domain.Services
{
    public class AuthorizationService
    {
        private User _currentUser;
        private UserService _userService;
        public  bool IsLogged { get; set; }

        public void LogIn(string login, string password)
        {
            _currentUser = _userService.Check(login, password);
            IsLogged = _currentUser != null;
        }
    }
}
