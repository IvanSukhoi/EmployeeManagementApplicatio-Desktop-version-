﻿using System.Windows;
using EmployeeManagement.UI.UiInterfaces;

namespace EmployeeManagement.UI.Services
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
