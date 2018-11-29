using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow
    {
        public AuthorizationWindow() { }

        public AuthorizationWindow(AuthorizationViewModel viewModel)
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
