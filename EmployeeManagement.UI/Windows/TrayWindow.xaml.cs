using System.Threading.Tasks;
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

        public async Task InitAsync()
        {
            await ((TrayViewModel) DataContext).InitAsync();
        }
    }
}
