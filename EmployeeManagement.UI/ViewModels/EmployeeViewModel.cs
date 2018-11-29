using EmployeeManagement.Contracts.Enums;
using Prism.Mvvm;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeViewModel: BindableBase
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                RaisePropertyChanged(nameof(MiddleName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        private Profession _profession;

        public Profession Profession
        {
            get => _profession;
            set
            {
                _profession = value;
                RaisePropertyChanged(nameof(Profession));
            }
        }

        private Position? _position;
        public Position? Position
        {
            get => _position;
            set
            {
                _position = value;
                RaisePropertyChanged(nameof(Position));
            }
        }

        public Sex Sex { get; set; }

        public int? ManagerId { get; set; }

        public int Id { get; set; }

        public int DepartmentId { get; set; }

        private string _departmentName;

        public string DepartmentName
        {
            get => _departmentName;
            set
            {
                _departmentName = value;
                RaisePropertyChanged(nameof(DepartmentName));
            }
        }

        public bool IsNew { get; set; }

        public bool IsEditedDepartment { get; set; }

        public bool IsDeleted { get; set; }
    }
}
