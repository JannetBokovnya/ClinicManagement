namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityIdNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "CityId", "dbo.Cities");
            DropIndex("dbo.Patients", new[] { "CityId" });
            DropPrimaryKey("dbo.Cities");
            AddColumn("dbo.Patients", "Cities_Id", c => c.Int());
            AlterColumn("dbo.Cities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cities", "Id");
            CreateIndex("dbo.Patients", "Cities_Id");
            AddForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Cities_Id", "dbo.Cities");
            DropIndex("dbo.Patients", new[] { "Cities_Id" });
            DropPrimaryKey("dbo.Cities");
            AlterColumn("dbo.Cities", "Id", c => c.Byte(nullable: false));
            DropColumn("dbo.Patients", "Cities_Id");
            AddPrimaryKey("dbo.Cities", "Id");
            CreateIndex("dbo.Patients", "CityId");
            AddForeignKey("dbo.Patients", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
