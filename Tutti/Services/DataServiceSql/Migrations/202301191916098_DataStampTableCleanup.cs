namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataStampTableCleanup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeStamps", "EntryDate", c => c.DateTime());
            AlterColumn("dbo.TimeStamps", "ExitDate", c => c.DateTime());
            DropColumn("dbo.TimeStamps", "Orphan");
            DropColumn("dbo.TimeStamps", "RecordValid");
            DropColumn("dbo.TimeStamps", "EditedManually");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeStamps", "EditedManually", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeStamps", "RecordValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeStamps", "Orphan", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TimeStamps", "ExitDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeStamps", "EntryDate", c => c.DateTime(nullable: false));
        }
    }
}
