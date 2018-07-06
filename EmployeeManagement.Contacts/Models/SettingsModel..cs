using EmployeeManagement.Contacts.Enums;

namespace EmployeeManagement.Contacts.Models
{
    public class SettingsModel
    {
        public int UserId { get; set; }
        public Theme Topic { get; set; }
        public Language Language { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
