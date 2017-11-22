namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGameClass : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Games", name: "Locaton_Id", newName: "Location_Id");
            RenameIndex(table: "dbo.Games", name: "IX_Locaton_Id", newName: "IX_Location_Id");
            AlterColumn("dbo.Games", "ChatId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "ChatId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Games", name: "IX_Location_Id", newName: "IX_Locaton_Id");
            RenameColumn(table: "dbo.Games", name: "Location_Id", newName: "Locaton_Id");
        }
    }
}
