using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmployeeManagement.DataEF.Enums;
using EmployeeManagement.Domain.DomainServices;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.DI.WindowFactory;
using EmployeeManagement.UI.Helpers;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;
        private readonly AuthorizationService _authorizationService;

        private readonly WindowFactory _windowFactory;

        public DataEF.Entities.Settings Settings { get; set; }

        public IDelegateCommand SelectTopicCommand { protected set; get; }
        public IDelegateCommand SelectLanguageCommand { protected set; get; }

        public IDelegateCommand RestartMainWindowCommand { protected set; get; }

        public IDelegateCommand BackToCurrentLanguageCommand { protected set; get; }

        public SettingsViewModel(SettingsService settingsService, AuthorizationService authorizationService, WindowFactory windowFactory)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            SelectTopicCommand = new DelegateCommand.DelegateCommand(ExecuteSelectTopic);
            SelectLanguageCommand = new DelegateCommand.DelegateCommand(ExecuteSelectLanguage);
            RestartMainWindowCommand = new DelegateCommand.DelegateCommand(ExecuteRestartMainWindow);
            BackToCurrentLanguageCommand = new DelegateCommand.DelegateCommand(BackToCurrentLanguage);
        }

        public void SetSettings()
        {
            Settings = _settingsService.GetById(_authorizationService.GetCurrentUser().ID);
            CurrentLanguage = Settings.Language;
        }

        private bool _isEditingLanguage;
        public bool IsEditingLanguage
        {
            get => _isEditingLanguage;
            set
            {
                _isEditingLanguage = value;
                OnPropertyChanged(nameof(IsEditingLanguage));
            }
        }

        public Language CurrentLanguage { get; set; }

        public void ExecuteSelectTopic(object parameter)
        {
            Settings.Topic = (Theme)parameter;
            OnPropertyChanged(nameof(Settings));

            SettingsHelper.SetTheme(Settings);

            _settingsService.Save(Settings);
        }

        public void ExecuteSelectLanguage(object parameter)
        {
            Settings.Language = (Language) parameter;
            OnPropertyChanged(nameof(Settings));

            IsEditingLanguage = true;

            _settingsService.Save(Settings);
        }

        public void ExecuteRestartMainWindow(object parameter)
        {
            _windowFactory.Close<MainWindow>();
            var mainWindow = _windowFactory.Create<MainWindow>();
            mainWindow.Init();
            mainWindow.Show();
        }

        public void BackToCurrentLanguage(object parameter)
        {
            Settings.Language = CurrentLanguage;

            IsEditingLanguage = false;

            OnPropertyChanged(nameof(Settings));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
