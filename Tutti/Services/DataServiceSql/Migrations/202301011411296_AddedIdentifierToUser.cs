namespace Services.DataServiceSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddedIdentifierToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Identifier", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "Identifier");
        }
    }
}
