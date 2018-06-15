using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.Domain.Models
{
    public class DepartmentModel
    {
        public Department Department { get; set; }
        public int QuantityEmployees { get; set; }
    }
}