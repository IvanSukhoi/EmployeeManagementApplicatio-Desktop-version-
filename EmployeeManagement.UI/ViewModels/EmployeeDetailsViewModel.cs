using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Domain.DomainInterfaces;
using EmployeeManagement.Domain.Mappings;
using EmployeeManagement.UI.Annotations;
using EmployeeManagement.UI.DelegateCommand;
using EmployeeManagement.UI.Settings.Localization;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        private readonly IMapperWrapper _mapperWrapper;

        public delegate void UpdateItemEventHandler();
        public event UpdateItemEventHandler UpdateEmployeeHandler;

        public IDelegateCommand EditCommand { protected set; get; }
        public IDelegateCommand CancelCommand { protected set; get; }
        public IDelegateCommand SaveCommand { protected set; get; }
        public IDelegateCommand OpenDeletePopupCommand { protected set; get; }
        public IDelegateCommand DeleteEmployeeCommand { protected set; get; }
        public IDelegateCommand CancelToListEmployeeCommand { protected set; get; }

        public List<DepartmentModel> Departments { get; set; }
        public List<Sex> SexTypes => Enum.GetValues(typeof(Sex)).Cast<Sex>().ToList();
        public List<Profession> ProfessionTypes => Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();
        public List<Position> PositionTypes => Enum.GetValues(typeof(Position)).Cast<Position>().ToList();

        public EmployeeDetailsViewModel(IEmployeeService employeeService, IDepartmentService departmentService, IMapperWrapper mapperWrapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapperWrapper = mapperWrapper;
            EditCommand = new DelegateCommand.DelegateCommand(ExecuteEditEmployee);
            CancelCommand = new DelegateCommand.DelegateCommand(ExecuteCancel);
            SaveCommand = new DelegateCommandAsync(ExecuteSaveEmployee);
            OpenDeletePopupCommand = new DelegateCommand.DelegateCommand(ExecuteOpenDeletePopup);
            DeleteEmployeeCommand = new DelegateCommandAsync(ExecuteDeleteEmployee);
            CancelToListEmployeeCommand = new DelegateCommand.DelegateCommand(ExecuteCancelTolistEmployee);
        }

        public EmployeeViewModel CurrentEmployeeViewModel { get; set; }

        private bool _isEditingEmployee;

        public bool IsEditingEmployee
        {
            get => _isEditingEmployee;
            set
            {
                _isEditingEmployee = value;
                OnPropertyChanged(nameof(IsEditingEmployee));
            }
        }

        private EmployeeViewModel _employeeViewModel;

        public EmployeeViewModel EmployeeViewModel
        {
            get => _employeeViewModel;
            set
            {
                _employeeViewModel = value;
                OnPropertyChanged(nameof(EmployeeViewModel));
            }
        }

        private bool _isDeletePopupOpen;

        public bool IsDeletePopupOpen
        {
            get => _isDeletePopupOpen;
            set
            {
                _isDeletePopupOpen = value;
                OnPropertyChanged(nameof(IsDeletePopupOpen));
            }
        }

        public async Task SetDepartments()
        {
            Departments = await _departmentService.GetAllAsync();
            Departments.ForEach(x => x.Name = Resource.ResourceManager.GetString(x.Name));
        }

        public void ExecuteEditEmployee(object parameter)
        {
            IsEditingEmployee = true;
        }

        public void ExecuteCancel(object parameter)
        {
            AssignEmployeeModel();
            IsEditingEmployee = false;
        }

        public async Task ExecuteSaveEmployee(object parameter)
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
            OnUpdateEmployee();
        }

        public void ExecuteOpenDeletePopup(object parameter)
        {
            IsDeletePopupOpen = true;
        }

        public async Task ExecuteDeleteEmployee(object parameter)
        {
            CurrentEmployeeViewModel.IsDeleted = true;
            await _employeeService.DeleteAsync((int)parameter);

            IsDeletePopupOpen = false;

            OnUpdateEmployee();
        }

        public void ExecuteCancelTolistEmployee(object parameter)
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnUpdateEmployee()
        {
            UpdateEmployeeHandler?.Invoke();
        }

        public virtual void AssignEmployeeModel()
        {
            if (CurrentEmployeeViewModel == null) return;

            EmployeeViewModel = _mapperWrapper.Map<EmployeeViewModel, EmployeeViewModel>(CurrentEmployeeViewModel);
        }
    }
}
