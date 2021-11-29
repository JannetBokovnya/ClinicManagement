namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnNameTablePatientDeleteCity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities");
            DropIndex("dbo.Patients", new[] { "Cities_Id" });
            AlterColumn("dbo.Patients", "CityId", c => c.Int(nullable: false));
            DropColumn("dbo.Patients", "Cities_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Cities_Id", c => c.Int());
            AlterColumn("dbo.Patients", "CityId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Patients", "Cities_Id");
            AddForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities", "Id");
        }
    }
}
