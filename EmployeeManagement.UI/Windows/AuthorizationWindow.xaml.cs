using System.Windows;
using EmployeeManagement.UI.Interfaces;

namespace EmployeeManagement.UI.Windows
{
    public partial class AuthorizationWindow : IWindow
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void CloseWindow()
        {
            DialogResult = true;
        }
    }
}
