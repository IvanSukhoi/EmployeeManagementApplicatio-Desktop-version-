namespace EmployeeManagement.DataEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingsEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        Topic = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "UserID", "dbo.User");
            DropIndex("dbo.Settings", new[] { "UserID" });
            DropTable("dbo.Settings");
        }
    }
}
