using EmployeeManagement.UI.UiInterfaces.ViewModels;
using EmployeeManagement.UI.UiInterfaces.Windows;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow : IAuthorizationWindow
    {
        public AuthorizationWindow() { }
        public AuthorizationWindow(IAuthorizationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public virtual bool? ShowDialogWindow()
        {
            return ShowDialog();
        }
    }
}
