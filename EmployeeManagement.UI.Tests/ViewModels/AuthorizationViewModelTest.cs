using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.ViewModels;
using EmployeeManagement.UI.Windows;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    public class AuthorizationViewModelTest
    {
        private IAuthorizationService _authorizationService;
        private IWindowFactory _windowFactory;
        private IDialogService _dialogService;
        private AuthorizationViewModel _authorizationViewModel;

        private string _messageBoxText;
        private string _messageBoxTitle;
        private bool _isLogged;
        private bool _isOpenWindow;

        [SetUp]
        public void SetUp()
        {
            _authorizationService = A.Fake<IAuthorizationService>();
            _windowFactory = A.Fake<IWindowFactory>();
            _dialogService = A.Fake<IDialogService>();
            _authorizationViewModel = new AuthorizationViewModel(_authorizationService, _windowFactory, _dialogService);

            Init();
        }

        [Test]
        public async Task ExecutePrintResultAuthorization_SuccessAuthorization_Correct()
        {
            A.CallTo(() => _authorizationService.LogInAsync(A<string>.Ignored, A<string>.Ignored, A<bool>.Ignored)).Invokes(
                () => { _isLogged = true; });

            await _authorizationViewModel.ExecutePrintResultAuthorizationAsync();

            Assert.That(_messageBoxTitle, Is.EqualTo("Successful Authorization!"));
            Assert.That(_messageBoxText, Is.EqualTo("Authorization"));
            Assert.IsTrue(_isLogged);
            Assert.IsFalse(_isOpenWindow);
        }

        [Test]
        public async Task ExecutePrintResultAuthorization_FailedAuthorization_Correct()
        {
            A.CallTo(() => _authorizationService.LogInAsync(A<string>.Ignored, A<string>.Ignored, A<bool>.Ignored)).Invokes(
                () => { _isLogged = false; });

            await _authorizationViewModel.ExecutePrintResultAuthorizationAsync();

            Assert.That(_messageBoxTitle, Is.EqualTo("Failed Authorization!"));
            Assert.That(_messageBoxText, Is.EqualTo("Authorization"));
            Assert.IsFalse(_isLogged);
        }

        public void Init()
        {
            _messageBoxText = null;
            _messageBoxTitle = null;
            _isLogged = false;
            _isOpenWindow = true;

            A.CallTo(() => _authorizationService.IsLogged).ReturnsLazily(() => _isLogged);

            A.CallTo(() => _dialogService.ShowMessageBox(A<string>.Ignored, A<string>.Ignored, MessageBoxButton.OK))
                .Invokes(
                    (string name, string inforamation, MessageBoxButton button) =>
                    {
                        _messageBoxTitle = name;
                        _messageBoxText = inforamation;
                    });

            A.CallTo(() => _windowFactory.Close<AuthorizationWindow>()).Invokes(() => _isOpenWindow = false);

            A.CallTo(() => _dialogService.ShowMessageBox(A<string>.Ignored, A<string>.Ignored))
                .Invokes(
                    (string name, string inforamation) =>
                    {
                        _messageBoxTitle = name;
                        _messageBoxText = inforamation;
                    });
        }
    }
}
