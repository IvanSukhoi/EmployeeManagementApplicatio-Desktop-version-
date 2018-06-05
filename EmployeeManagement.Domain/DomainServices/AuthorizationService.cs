using EmployeeManagement.DataEF;
using EmployeeManagement.Domain.Helpers;

namespace EmployeeManagement.Domain.DomainServices
{
    public class AuthorizationService
    {
        private User _currentUser;
        public bool IsLogged => _currentUser != null;
        public bool IsRemembered { get; set; }

        private readonly UserService _userService;
        
        public AuthorizationService(UserService userService)
        {
            _userService = userService;
        }

        public void LogIn(string login, string password, bool rememberMe)
        {
            _currentUser = _userService.GetUser(login, password);
            
            if (_currentUser != null)
            {
                if (rememberMe)
                {
                    IsRemembered = true;
                    RegistryHelper.SetData("Login", login);
                    RegistryHelper.SetData("Password", password);
                }
            }
        }

        public void LogOut()
        {
            _currentUser = null;
            if (IsRemembered)
            {
                RegistryHelper.RemoveData("Login", "Password");
            }
        }

        public bool IsAuthorized()
        {
            _currentUser = _userService.GetUser(RegistryHelper.GetData("Login"), RegistryHelper.GetData("Password"));
            if (_currentUser != null)
            {
                IsRemembered = true;
            }

            return IsLogged;
        }
    }
}
