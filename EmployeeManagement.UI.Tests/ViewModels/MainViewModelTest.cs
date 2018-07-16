using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace EmployeeManagement.UI.Tests.ViewModels
{
    public class MainViewModelTest
    {
        private INavigationManager _navigationManager;
        private MainViewModel _mainViewModel;

        [SetUp]
        public void SetUp()
        {
            _navigationManager = A.Fake<INavigationManager>();
            _mainViewModel = new MainViewModel(_navigationManager);
        }

        [Test]
        public async Task ExecuteSelectByDepartmentAsync_InvokeNavigate_Correct()
        {
            object o = new object[]{ Contracts.Enums.Pages.HomePage, Departments.Design };

            await _mainViewModel.ExecuteSelectByDepartmentAsync(o);

            A.CallTo(() => _navigationManager.Navigate(A<Contracts.Enums.Pages>.Ignored, A<Departments>.Ignored))
                .MustHaveHappened();
            Assert.That(_mainViewModel.CurrentPage, Is.EqualTo(Contracts.Enums.Pages.HomePage));
            Assert.That(_mainViewModel.CurrentDepartment, Is.EqualTo(Departments.Design));
        }

        [Test]
        public async Task ExecuteChangeSettingsAsync_ChangeSettings_Correct()
        {
            await _mainViewModel.ExecuteChangeSettingsAsync(Contracts.Enums.Pages.SettingsPage);

            A.CallTo(() => _navigationManager.Navigate(A<Contracts.Enums.Pages>.Ignored, A<Departments>.Ignored))
                .MustHaveHappened();
            Assert.That(_mainViewModel.CurrentPage, Is.EqualTo(Contracts.Enums.Pages.SettingsPage));
            Assert.That(_mainViewModel.CurrentDepartment, Is.EqualTo(Departments.NotSelected));
        }
    }
}
