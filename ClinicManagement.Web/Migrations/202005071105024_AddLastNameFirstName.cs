namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastNameFirstName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.Patients", "FirstName", c => c.String());
            AddColumn("dbo.Patients", "LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.Patients", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropColumn("dbo.Patients", "LastName");
            DropColumn("dbo.Patients", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Doctors", "FirstName");
        }
    }
}
