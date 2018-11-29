using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;
using EmployeeManagement.UI.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace EmployeeManagement.UI.ViewModels
{
    public class SettingsViewModel: BindableBase, IRegionMemberLifetime
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;

        private readonly IWindowFactory _windowFactory;

        private readonly ISettingsHelper _settingsHelper;

        private readonly IWindowService _windowService;

        private SettingsModel _settingsModel;
        public SettingsModel SettingsModel
        {
            get => _settingsModel;
            set
            {
                _settingsModel = value;
                RaisePropertyChanged(nameof(SettingsModel));
            }
        }

        public ICommand SelectTopicCommand { protected set; get; }
        public ICommand SelectLanguageCommand { protected set; get; }
        public ICommand RestartMainWindowCommand { protected set; get; }
        public ICommand BackToCurrentLanguageCommand { protected set; get; }

        public SettingsViewModel(ISettingsService settingsService, IAuthorizationService authorizationService, IWindowFactory windowFactory, ISettingsHelper settingsHelper, IWindowService windowService)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _windowFactory = windowFactory;
            _settingsHelper = settingsHelper;
            _windowService = windowService;
            SelectTopicCommand = new DelegateCommand<Theme?>(async(_) => await ExecuteSelectThemeAsync(_));
            SelectLanguageCommand = new DelegateCommand<Language?>(async (_) => await ExecuteSelectLanguageAsync(_));
            RestartMainWindowCommand = new DelegateCommand(async () => await ExecuteRestartMainWindowAsync());
            BackToCurrentLanguageCommand = new DelegateCommand(BackToCurrentLanguage);
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
                RaisePropertyChanged(nameof(IsEditingLanguage));
            }
        }

        public Language CurrentLanguage { get; set; }

        public async Task ExecuteSelectThemeAsync(Theme? parameter)
        {
            if (parameter != null) SettingsModel.Theme = (Theme) parameter;
            RaisePropertyChanged(nameof(SettingsModel));

            _settingsHelper.SetTheme(SettingsModel);

            await _settingsService.SaveAsync(SettingsModel);
        }

        public async Task ExecuteSelectLanguageAsync(Language? parameter)
        {
            if (parameter != null) SettingsModel.Language = (Language) parameter;
            RaisePropertyChanged(nameof(SettingsModel));

            IsEditingLanguage = true;

            await _settingsService.SaveAsync(SettingsModel);
        }

        public async Task ExecuteRestartMainWindowAsync()
        {
            _windowFactory.Close<MainWindow>();

            await _windowService.CreateMainWindowAsync();
        }

        public void BackToCurrentLanguage()
        {
            SettingsModel.Language = CurrentLanguage;

            IsEditingLanguage = false;

            RaisePropertyChanged(nameof(SettingsModel));
        }

        public bool KeepAlive => false;
    }
}
