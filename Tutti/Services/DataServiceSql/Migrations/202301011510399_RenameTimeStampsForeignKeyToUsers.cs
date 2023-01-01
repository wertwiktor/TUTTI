namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTimeStampsForeignKeyToUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeStamps", "User_Id", "dbo.Users");
            DropIndex("dbo.TimeStamps", new[] { "User_Id" });
            RenameColumn(table: "dbo.TimeStamps", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.TimeStamps", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.TimeStamps", "UserId");
            AddForeignKey("dbo.TimeStamps", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeStamps", "UserId", "dbo.Users");
            DropIndex("dbo.TimeStamps", new[] { "UserId" });
            AlterColumn("dbo.TimeStamps", "UserId", c => c.Long());
            RenameColumn(table: "dbo.TimeStamps", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.TimeStamps", "User_Id");
            AddForeignKey("dbo.TimeStamps", "User_Id", "dbo.Users", "Id");
        }
    }
}
