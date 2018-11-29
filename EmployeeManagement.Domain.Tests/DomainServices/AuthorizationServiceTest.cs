using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.API.ApiInterfaces;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Settings;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.DomainServices;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.Domain.Tests.DomainServices
{
    public class AuthorizationServiceTest
    {
        private IUserService _userService;
        private IRegistryManager _registryManager;
        private IAuthorizationManager _authorizationManager;

        private Dictionary<string, string> _registerDictionary;

        private AuthorizationService _authorizationService;
        private List<UserModel> _userModels;

        [SetUp]
        public void SetUp()
        {
            _userService = A.Fake<IUserService>();
            _registryManager = A.Fake<IRegistryManager>();
            _authorizationManager = A.Fake<IAuthorizationManager>();

            _authorizationService = new AuthorizationService(_userService, _registryManager, _authorizationManager);
            Init();
        }

        [Test]
        public async Task LogInAsync_()
        {
            var userModel = new UserModel
            {
                Login = "Login1",
                Password = "Password1"
            };

            A.CallTo(() => _authorizationManager.IsAuthorized()).Returns(true);

            A.CallTo(() => _authorizationManager.SetAuthorizationAsync(userModel))
                .MustHaveHappenedOnceExactly();

            var expectedValue = _authorizationService.GetCurrentUser();

            Assert.That(expectedValue, Is.EqualTo(_userModels.SingleOrDefault(x => x.Login == userModel.Login)));
        }

        [Test]
        public async Task LogOut_RemoveCurrentUser_Correct()
        {
            A.CallTo(() => _registryManager.RemoveData(SettingsConfiguration.RegistrySettings.RefreshToken)).Invokes(() =>
            {
                _registerDictionary.Remove(SettingsConfiguration.RegistrySettings.RefreshToken);
            });

            await _authorizationService.LogInAsync("Login1", "Password1", true);
            _authorizationService.LogOut();

            Assert.IsFalse(_registerDictionary.ContainsKey(SettingsConfiguration.RegistrySettings.RefreshToken));
            Assert.That(_authorizationService.GetCurrentUser(), Is.Null);
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

            A.CallTo(() => _userService.GetByLoginAsync(A<string>.Ignored)).
                ReturnsLazily((string login) => _userModels.FirstOrDefault(x => x.Login == login));

            A.CallTo(() => _registryManager.SetData(SettingsConfiguration.RegistrySettings.RefreshToken, A<string>.Ignored)).Invokes((string name, string value) =>
            {
                _registerDictionary.Add(name, value);
            });
        }

        //[Test]
        //public async Task IsAuthorized_True_Correct()
        //{
        //    await _authorizationService.LogInAsync("Login1", "Password1", true);

        //    A.CallTo(() => _registryManager.GetData(SettingsConfiguration.RegistrySettings.RefreshToken))
        //        .Returns(_registerDictionary[SettingsConfiguration.RegistrySettings.RefreshToken]);

        //    Assert.IsTrue(_authorizationService.IsLogged);
        //}

        //[Test]
        //public async Task IsAuthorized_False_Correct()
        //{
        //    await _authorizationService.IsAuthorized();

        //    Assert.IsFalse(_authorizationService.IsLogged);
        //}

        //[Test]
        //public async Task LogInAsync_AddUserInformationToRegister_Correct()
        //{
        //    await _authorizationService.LogInAsync("Login1", "Password1", true);

        //    Assert.That(_authorizationService.GetCurrentUser(), Is.EqualTo(_userModels.FirstOrDefault(x => x.Login == "Login1" && x.Password == "Password1")));
        //    Assert.That(_registerDictionary["Login"], Is.EqualTo("Login1"));
        //    Assert.That(_registerDictionary["Password"], Is.EqualTo("Password1"));
        //    Assert.That(_authorizationService.IsRemembered, Is.EqualTo(true));
        //}

        //[Test]
        //public async Task LogInAsync_AssignCurrentUser_Correct()
        //{
        //    await _authorizationService.LogInAsync("Login1", "Password1", false);

        //    Assert.That(_authorizationService.GetCurrentUser(), Is.EqualTo(_userModels.FirstOrDefault(x => x.Login == "Login1" && x.Password == "Password1")));
        //    Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
        //    Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
        //    Assert.That(_authorizationService.IsRemembered, Is.EqualTo(false));
        //}

        //[Test]
        //public async Task LogInAsync_InCorrect()
        //{
        //    await _authorizationService.LogInAsync("Login3", "Password1", false);

        //    Assert.That(_authorizationService.GetCurrentUser(), Is.Null);
        //    Assert.IsFalse(_registerDictionary.ContainsKey("Login"));
        //    Assert.IsFalse(_registerDictionary.ContainsKey("Password"));
        //    Assert.That(_authorizationService.IsRemembered, Is.EqualTo(false));
        //}


    }
}
