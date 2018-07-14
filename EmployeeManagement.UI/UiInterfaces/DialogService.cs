using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagement.UI.UiInterfaces
{
    public class DialogService : IDialogService
    {
        public void ShowMessageBox(string title, string text)
        {
            MessageBox.Show(text, title);
        }

        public void ShowMessageBox(string title, string text, MessageBoxButton button)
        {
            MessageBox.Show(text, title, button);
        }
    }
}
