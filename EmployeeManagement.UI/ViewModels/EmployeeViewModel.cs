﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.UI.Annotations;

namespace EmployeeManagement.UI.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private Profession _profession;

        public Profession Profession
        {
            get => _profession;
            set
            {
                _profession = value;
                OnPropertyChanged(nameof(Profession));
            }
        }

        private Position? _position;
        public Position? Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
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
                OnPropertyChanged(nameof(DepartmentName));
            }
        }

        public bool IsNew { get; set; }

        public bool IsEditedDepartment { get; set; }

        public bool IsDeleted { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
