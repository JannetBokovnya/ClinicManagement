namespace ClinicManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableAdministrationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdministrationModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdministrationModels");
        }
    }
}
