namespace Services.DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeStamps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Direction = c.Int(nullable: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeStamps", "User_Id", "dbo.Users");
            DropIndex("dbo.TimeStamps", new[] { "User_Id" });
            DropTable("dbo.TimeStamps");
            DropTable("dbo.Users");
        }
    }
}
