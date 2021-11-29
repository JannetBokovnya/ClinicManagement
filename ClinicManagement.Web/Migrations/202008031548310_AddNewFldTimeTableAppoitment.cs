namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFldTimeTableAppoitment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "TimeBlockHelper", c => c.String());
            DropColumn("dbo.Appointments", "StartDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "StartDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "TimeBlockHelper");
            DropColumn("dbo.Appointments", "Time");
            DropColumn("dbo.Appointments", "Date");
        }
    }
}
