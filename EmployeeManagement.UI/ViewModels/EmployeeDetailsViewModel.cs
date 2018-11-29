using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Events;
using EmployeeManagement.UI.Settings.Localization;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeDetailsViewModel : BindableBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        private readonly IMapperWrapper _mapperWrapper;

        private readonly IEventAggregator _eventAggregator;

        public ICommand EditCommand { protected set; get; }
        public ICommand CancelCommand { protected set; get; }
        public ICommand SaveCommand { protected set; get; }
        public ICommand OpenDeletePopupCommand { protected set; get; }
        public ICommand DeleteEmployeeCommand { protected set; get; }
        public ICommand CancelToListEmployeeCommand { protected set; get; }

        public List<DepartmentModel> Departments { get; set; }
        public List<Sex> SexTypes => Enum.GetValues(typeof(Sex)).Cast<Sex>().ToList();
        public List<Profession> ProfessionTypes => Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();
        public List<Position> PositionTypes => Enum.GetValues(typeof(Position)).Cast<Position>().ToList();

        public EmployeeDetailsViewModel(IEmployeeService employeeService, IDepartmentService departmentService, IMapperWrapper mapperWrapper, IEventAggregator eventAggregator)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
            _eventAggregator = eventAggregator;
            EditCommand = new DelegateCommand(async () => await ExecuteEditEmployeeAsync());
            CancelCommand = new DelegateCommand(ExecuteCancel);
            SaveCommand = new DelegateCommand(async () => await ExecuteSaveEmployee());
            OpenDeletePopupCommand = new DelegateCommand(ExecuteOpenDeletePopup);
            DeleteEmployeeCommand = new DelegateCommand<int?>(async (x) => await ExecuteDeleteEmployeeAsync(x));
            CancelToListEmployeeCommand = new DelegateCommand(ExecuteCancelToListEmployee);
        }

        public void SubscribeToTheEvent()
        {
            _eventAggregator.GetEvent<UpdateEmployeeViewModelEvent>().Subscribe(SetEmployeeHandler);
        }

        public EmployeeViewModel CurrentEmployeeViewModel { get; set; }

        private bool _isEditingEmployee;

        public bool IsEditingEmployee
        {
            get => _isEditingEmployee;
            set
            {
                _isEditingEmployee = value;
                RaisePropertyChanged(nameof(IsEditingEmployee));
            }
        }

        private EmployeeViewModel _employeeViewModel;

        public EmployeeViewModel EmployeeViewModel
        {
            get => _employeeViewModel;
            set
            {
                _employeeViewModel = value;
                RaisePropertyChanged(nameof(EmployeeViewModel));
            }
        }

        private bool _isDeletePopupOpen;

        public bool IsDeletePopupOpen
        {
            get => _isDeletePopupOpen;
            set
            {
                _isDeletePopupOpen = value;
                RaisePropertyChanged(nameof(IsDeletePopupOpen));
            }
        }

        public async Task ExecuteEditEmployeeAsync()
        {
            Departments = await _departmentService.GetAllAsync();
            Departments.ForEach(x => x.Name = Resource.ResourceManager.GetString(x.Name));

            IsEditingEmployee = true;
        }

        public void ExecuteCancel()
        {
            AssignEmployeeModel();
            IsEditingEmployee = false;
        }

        public async Task ExecuteSaveEmployee()
        {
            if (EmployeeViewModel.IsNew)
            {
                var employee = await _employeeService.CreateAsync(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(EmployeeViewModel));
                _mapperWrapper.Map(employee, EmployeeViewModel);
            }
            else
            {
                await _employeeService.SaveAsync(_mapperWrapper.Map<EmployeeViewModel, EmployeeModel>(EmployeeViewModel));

                if (EmployeeViewModel.DepartmentId != CurrentEmployeeViewModel.DepartmentId)
                {
                    EmployeeViewModel.IsEditedDepartment = true;
                }
            }

            IsEditingEmployee = false;
            _mapperWrapper.Map(EmployeeViewModel, CurrentEmployeeViewModel);

            _eventAggregator.GetEvent<SaveEmployeeViewModelEvent>().Publish();
        }

        public void ExecuteOpenDeletePopup()
        {
            IsDeletePopupOpen = true;
        }

        public async Task ExecuteDeleteEmployeeAsync(int? employeeId)
        {
            CurrentEmployeeViewModel.IsDeleted = true;
            await _employeeService.DeleteAsync((int)employeeId);

            IsDeletePopupOpen = false;

            _eventAggregator.GetEvent<SaveEmployeeViewModelEvent>().Publish();
        }

        public void ExecuteCancelToListEmployee()
        {
            IsDeletePopupOpen = false;
        }

        public void SetEmployeeHandler(EmployeeViewModel employee)
        {
            CurrentEmployeeViewModel = employee;
            AssignEmployeeModel();

            if (employee != null)
            {
                IsEditingEmployee = employee.IsNew;
            }
        }

        public virtual void AssignEmployeeModel()
        {
            if (CurrentEmployeeViewModel == null) return;

            EmployeeViewModel = _mapperWrapper.Map<EmployeeViewModel, EmployeeViewModel>(CurrentEmployeeViewModel);
        }
    }
}
