using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmployeeManagement.DataEF.Entities;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Helpers;

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
            Settings = _settingsService.GetById(_authorizationService.GetCurrentUser().ID);
        }

        public void ExecuteSelectTopic(object parameter)
        {
            Settings.Topic = (Theme)parameter;
            OnPropertyChanged(nameof(Settings));

            SettingsHelper.SetTheme(Settings);

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
