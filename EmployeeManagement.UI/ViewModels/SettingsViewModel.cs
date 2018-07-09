using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
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
        
        private SettingsModel _settingsModel;
        public SettingsModel SettingsModel
        {
            get => _settingsModel;
            set
            {
                _settingsModel = value;
                OnPropertyChanged(nameof(SettingsModel));
            }
        }

        public IDelegateCommand SelectTopicCommand { protected set; get; }
        public IDelegateCommand SelectLanguageCommand { protected set; get; }
        public IDelegateCommand RestartMainWindowCommand { protected set; get; }
        public IDelegateCommand BackToCurrentLanguageCommand { protected set; get; }

        public SettingsViewModel(SettingsService settingsService, AuthorizationService authorizationService, WindowFactory windowFactory)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            SelectTopicCommand = new DelegateCommandAsync(ExecuteSelectTopicAsync);
            SelectLanguageCommand = new DelegateCommandAsync(ExecuteSelectLanguageAsync);
            RestartMainWindowCommand = new DelegateCommandAsync(ExecuteRestartMainWindowAsync);
            BackToCurrentLanguageCommand = new DelegateCommand.DelegateCommand(BackToCurrentLanguage);
        }

        public async Task SetSettings()
        {
            SettingsModel = await _settingsService.GetByIdAsync(_authorizationService.GetCurrentUser().Id);
            CurrentLanguage = SettingsModel.Language;
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

        public async Task ExecuteSelectTopicAsync(object parameter)
        {
            SettingsModel.Topic = (Theme)parameter;
            OnPropertyChanged(nameof(SettingsModel));

            SettingsHelper.SetTheme(SettingsModel);

            await _settingsService.SaveAsync(SettingsModel);
        }

        public async Task ExecuteSelectLanguageAsync(object parameter)
        {
            SettingsModel.Language = (Language)parameter;
            OnPropertyChanged(nameof(SettingsModel));

            IsEditingLanguage = true;

            await _settingsService.SaveAsync(SettingsModel);
        }

        public async Task ExecuteRestartMainWindowAsync(object parameter)
        {
            _windowFactory.Close<MainWindow>();
            SettingsHelper.SetLanguage(await _settingsService.GetByIdAsync(_authorizationService.GetCurrentUser().Id));
            var mainWindow = _windowFactory.Create<MainWindow>();
            await mainWindow.InitAsync();
            mainWindow.Show();
        }

        public void BackToCurrentLanguage(object parameter)
        {
            SettingsModel.Language = CurrentLanguage;

            IsEditingLanguage = false;

            OnPropertyChanged(nameof(SettingsModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
