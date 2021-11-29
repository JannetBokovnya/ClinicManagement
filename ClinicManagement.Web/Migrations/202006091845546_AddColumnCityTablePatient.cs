namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCityTablePatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "CityId");
            AddForeignKey("dbo.Patients", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "CityId", "dbo.Cities");
            DropIndex("dbo.Patients", new[] { "CityId" });
            DropColumn("dbo.Patients", "CityId");
        }
    }
}
