namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEditedEntryAndExitInTimestampsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeStamps", "EditedEntryDate", c => c.DateTime());
            AddColumn("dbo.TimeStamps", "EditedExitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeStamps", "EditedExitDate");
            DropColumn("dbo.TimeStamps", "EditedEntryDate");
        }
    }
}
