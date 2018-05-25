using EmployeeManagement.UI.Interfaces;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class TrayWindow : IWindow
    {
        public TrayWindow(TrayViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public void Init()
        {
            ((TrayViewModel)DataContext).Init();
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
