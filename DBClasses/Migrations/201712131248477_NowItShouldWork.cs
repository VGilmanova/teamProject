namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NowItShouldWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Ints", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Ints");
        }
    }
}
