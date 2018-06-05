using System.Windows;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow
    {
        public AuthorizationWindow(AuthorizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
