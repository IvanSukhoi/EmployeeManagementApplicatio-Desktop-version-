using EmployeeManagement.UI.UiInterfaces.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow
    {
        public AuthorizationWindow(IAuthorizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
