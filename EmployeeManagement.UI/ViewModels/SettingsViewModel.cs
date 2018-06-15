using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;

namespace EmployeeManagement.UI.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;
        private readonly AuthorizationService _authorizationService;

        public Settings Settings { get; set; }

        public IDelegateCommand SelectTopicCommand { protected set; get; }

        public SettingsViewModel(SettingsService settingsService, AuthorizationService authorizationService)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            SelectTopicCommand = new DelegateCommand.DelegateCommand(ExecuteSelectTopic);
        }

        public void SetSettings()
        {
            Settings = _settingsService.GetById(_authorizationService.GetCureentUser().ID);
        }

        public void ExecuteSelectTopic(object parameter)
        {
         
            Settings.Topic = (Theme)parameter;
            OnPropertyChanged(nameof(Settings));

            string style = Settings.Topic.ToString();

            //TODO duplicate
            var uri = new Uri("Settings/Themes/" + style + ".xaml", UriKind.Relative);

            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            _settingsService.Save(Settings);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
