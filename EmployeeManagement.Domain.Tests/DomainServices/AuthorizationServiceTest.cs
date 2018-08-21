using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.DomainServices;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class AuthorizationServiceTest
    {
        private IUserService _userService;
        private IRegistryHelper _registryHelper;
        private Dictionary<string, string> _registerDictionary;
        private AuthorizationService _authorizationService;
        private List<UserModel> _userModels;

        [SetUp]
        public void SetUp()
        {
            _userService = A.Fake<IUserService>();
            _registryHelper = A.Fake<IRegistryHelper>();
            _authorizationService = new AuthorizationService(_userService, _registryHelper);
            Init();
        }

        [Test]
        public async Task LogOut_RemoveCurrentUserOfRegistry_Correct()
        {
            A.CallTo(() => _registryHelper.RemoveData("Login", "Password")).Invokes(() =>
            {
                _registerDictionary.Remove("Login");
                _registerDictionary.Remove("Password");
            });

            await _authorizationService.LogInAsync("Loign1", "Password1", true);
            _authorizationService.LogOut();

            Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
            Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
            Assert.That(_authorizationService.GetCurrentUser(), Is.Null);
        }

        [Test]
        public async Task LogOut_RemoveCurrentUser_Correct()
        {
            await _authorizationService.LogInAsync("Loign1", "Password1", false);
            _authorizationService.LogOut();

            Assert.That(_authorizationService.GetCurrentUser(), Is.Null);
            Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
            Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
        }

        [Test]
        public async Task IsAuthorized_True_Correct()
        {
            await _authorizationService.LogInAsync("Login1", "Password1", true);

            A.CallTo(() => _registryHelper.GetData("Login")).Returns(_registerDictionary["Login"]);
            A.CallTo(() => _registryHelper.GetData("Password")).Returns(_registerDictionary["Password"]);

            Assert.IsTrue(_authorizationService.IsLogged);
        }

        [Test]
        public async Task IsAuthorized_False_Correct()
        {
            await _authorizationService.IsAuthorized();

            Assert.IsFalse(_authorizationService.IsLogged);
        }

        [Test]
        public async Task LogInAsyc_AddUserInformationToRegister_Correct()
        {
            await _authorizationService.LogInAsync("Login1", "Password1", true);

            Assert.That(_authorizationService.GetCurrentUser(), Is.EqualTo(_userModels.FirstOrDefault(x => x.Login == "Login1" && x.Password == "Password1")));
            Assert.That(_registerDictionary["Login"], Is.EqualTo("Login1"));
            Assert.That(_registerDictionary["Password"], Is.EqualTo("Password1"));
            Assert.That(_authorizationService.IsRemembered, Is.EqualTo(true));
        }

        [Test]
        public async Task LogInAsync_AssignCurrentUser_Correct()
        {
            await _authorizationService.LogInAsync("Login1", "Password1", false);

            Assert.That(_authorizationService.GetCurrentUser(), Is.EqualTo(_userModels.FirstOrDefault(x => x.Login == "Login1" && x.Password == "Password1")));
            Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
            Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
            Assert.That(_authorizationService.IsRemembered, Is.EqualTo(false));
        }

        [Test]
        public async Task LogInAsync_InCorrect()
        {
            await _authorizationService.LogInAsync("Login3", "Password1", false);

            Assert.That(_authorizationService.GetCurrentUser(), Is.Null);
            Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
            Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
            Assert.That(_authorizationService.IsRemembered, Is.EqualTo(false));
        }

        public void Init()
        {
            _registerDictionary = new Dictionary<string, string>();

            _userModels = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Login = "Login1",
                    Password = "Password1",
                    SettingsModel = new SettingsModel()
                },
                new UserModel
                {
                    Id = 2,
                    Login = "Login2",
                    Password = "Password2",
                    SettingsModel = new SettingsModel()
                }
            };

            A.CallTo(() => _userService.GetByLoginAsync(A<string>.Ignored, A<string>.Ignored)).
                ReturnsLazily((string login, string password) => _userModels.FirstOrDefault(x => x.Login == login && x.Password == password));

            A.CallTo(() => _registryHelper.SetData("Login", A<string>.Ignored)).Invokes((string name, string login) =>
            {
                _registerDictionary.Add(name, login);
            });

            A.CallTo(() => _registryHelper.SetData("Password", A<string>.Ignored)).Invokes((string name, string password) =>
            {
                _registerDictionary.Add(name, password);
            });
        }
    }
}
