using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.Contracts.Models
{
    public class SettingsModel
    {
        public int UserId { get; set; }
        public Theme Theme { get; set; }
        public Language Language { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
