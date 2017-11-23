namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationschema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ToLocation_Id", "dbo.Locations");
            AddColumn("dbo.Answers", "Location_Id", c => c.Int());
            AddColumn("dbo.Locations", "Answer_1_Id", c => c.Int());
            AddColumn("dbo.Locations", "Answer_2_Id", c => c.Int());
            AddColumn("dbo.Locations", "Answer_3_Id", c => c.Int());
            AddColumn("dbo.Locations", "Answer_4_Id", c => c.Int());
            CreateIndex("dbo.Answers", "Location_Id");
            CreateIndex("dbo.Locations", "Answer_1_Id");
            CreateIndex("dbo.Locations", "Answer_2_Id");
            CreateIndex("dbo.Locations", "Answer_3_Id");
            CreateIndex("dbo.Locations", "Answer_4_Id");
            AddForeignKey("dbo.Locations", "Answer_1_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Locations", "Answer_2_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Locations", "Answer_3_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Locations", "Answer_4_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Answers", "Location_Id", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Answer_4_Id", "dbo.Answers");
            DropForeignKey("dbo.Locations", "Answer_3_Id", "dbo.Answers");
            DropForeignKey("dbo.Locations", "Answer_2_Id", "dbo.Answers");
            DropForeignKey("dbo.Locations", "Answer_1_Id", "dbo.Answers");
            DropIndex("dbo.Locations", new[] { "Answer_4_Id" });
            DropIndex("dbo.Locations", new[] { "Answer_3_Id" });
            DropIndex("dbo.Locations", new[] { "Answer_2_Id" });
            DropIndex("dbo.Locations", new[] { "Answer_1_Id" });
            DropIndex("dbo.Answers", new[] { "Location_Id" });
            DropColumn("dbo.Locations", "Answer_4_Id");
            DropColumn("dbo.Locations", "Answer_3_Id");
            DropColumn("dbo.Locations", "Answer_2_Id");
            DropColumn("dbo.Locations", "Answer_1_Id");
            DropColumn("dbo.Answers", "Location_Id");
            AddForeignKey("dbo.Answers", "ToLocation_Id", "dbo.Locations", "Id");
        }
    }
}
