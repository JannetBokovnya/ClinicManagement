namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnNameTableDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "LastName", c => c.String());
            DropColumn("dbo.Doctors", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Name", c => c.String());
            DropColumn("dbo.Doctors", "LastName");
        }
    }
}
