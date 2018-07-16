using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.Windows;

namespace EmployeeManagement.UI.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;

        private readonly IWindowFactory _windowFactory;

        private readonly ISettingsHelper _settingsHelper;
        
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

        public SettingsViewModel(ISettingsService settingsService, IAuthorizationService authorizationService, IWindowFactory windowFactory, ISettingsHelper settingsHelper)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _settingsHelper = settingsHelper;
            SelectTopicCommand = new DelegateCommandAsync(ExecuteSelectThemeAsync);
            SelectLanguageCommand = new DelegateCommandAsync(ExecuteSelectLanguageAsync);
            RestartMainWindowCommand = new DelegateCommandAsync(ExecuteRestartMainWindowAsync);
            BackToCurrentLanguageCommand = new DelegateCommand.DelegateCommand(BackToCurrentLanguage);
        }

        public async Task SetSettings()
        {
            SettingsModel = await _settingsService.GetByUserIdAsync(_authorizationService.GetCurrentUser().Id);
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

        public async Task ExecuteSelectThemeAsync(object parameter)
        {
            SettingsModel.Theme = (Theme)parameter;
            OnPropertyChanged(nameof(SettingsModel));

            _settingsHelper.SetTheme(SettingsModel);

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
            _settingsHelper.SetLanguage(await _settingsService.GetByUserIdAsync(_authorizationService.GetCurrentUser().Id));
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
