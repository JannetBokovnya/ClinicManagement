namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnCityTablePatient : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities");
            //DropIndex("dbo.Patients", new[] { "Cities_Id" });
            //DropColumn("dbo.Patients", "CityId");
            //RenameColumn(table: "dbo.Patients", name: "Cities_Id", newName: "CityId");
            AlterColumn("dbo.Patients", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Patients", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "CityId");
            AddForeignKey("dbo.Patients", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "CityId", "dbo.Cities");
            DropIndex("dbo.Patients", new[] { "CityId" });
            AlterColumn("dbo.Patients", "CityId", c => c.Int());
            //AlterColumn("dbo.Patients", "CityId", c => c.Byte(nullable: false));
            //RenameColumn(table: "dbo.Patients", name: "CityId", newName: "Cities_Id");
            //AddColumn("dbo.Patients", "CityId", c => c.Byte(nullable: false));
            //CreateIndex("dbo.Patients", "Cities_Id");
            //AddForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities", "Id");
        }
    }
}
