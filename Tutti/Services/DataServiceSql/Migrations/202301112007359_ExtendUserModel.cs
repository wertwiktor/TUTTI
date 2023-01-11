namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Surname", c => c.String());
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Nationality", c => c.String());
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "Nationality");
            DropColumn("dbo.Users", "Level");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "DateOfBirth");
            DropColumn("dbo.Users", "Surname");
        }
    }
}
