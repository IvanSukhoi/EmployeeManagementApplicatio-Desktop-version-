namespace EmployeeManagement.DataEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "Position", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "Position", c => c.Int(nullable: false));
        }
    }
}
