using EmployeeManagement.UI.Interfaces;

namespace EmployeeManagement.UI.Windows
{
    public partial class TrayWindow : IWindow
    {
        public TrayWindow()
        {
            InitializeComponent();
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
