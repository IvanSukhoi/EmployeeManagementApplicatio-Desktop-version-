using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class TrayWindow
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
    }
}
