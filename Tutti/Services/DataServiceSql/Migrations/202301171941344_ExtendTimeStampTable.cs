namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendTimeStampTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeStamps", "EntryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TimeStamps", "ExitDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TimeStamps", "Orphan", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeStamps", "RecordValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeStamps", "EditedManually", c => c.Boolean(nullable: false));
            DropColumn("dbo.TimeStamps", "DateTime");
            DropColumn("dbo.TimeStamps", "Direction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeStamps", "Direction", c => c.Int(nullable: false));
            AddColumn("dbo.TimeStamps", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.TimeStamps", "EditedManually");
            DropColumn("dbo.TimeStamps", "RecordValid");
            DropColumn("dbo.TimeStamps", "Orphan");
            DropColumn("dbo.TimeStamps", "ExitDate");
            DropColumn("dbo.TimeStamps", "EntryDate");
        }
    }
}
