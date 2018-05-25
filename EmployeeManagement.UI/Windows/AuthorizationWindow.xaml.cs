using System.Windows;
using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow : IWindow
    {
        public AuthorizationWindow(AuthorizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void CloseWindow()
        {
            DialogResult = true;
        }
    }
}
