using EmployeeManagement.Contacts.Enums;

namespace EmployeeManagement.Contacts.Models
{
    public class SettringsModel
    {
        public int UserId { get; set; }
        public Theme Topic { get; set; }
        public Language Language { get; set; }
        public UserModel User { get; set; }
    }
}
