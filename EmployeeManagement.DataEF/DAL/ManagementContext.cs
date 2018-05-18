using System.Data.Entity;

namespace EmployeeManagement.DataEF.DAL
{
    public partial class ManagementContext : DbContext
    {
        public ManagementContext() : base("name=ManagementContext")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
