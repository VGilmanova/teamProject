namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswersFrom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PostDescrption = c.String(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Long(nullable: false),
                        WorkTime = c.Time(nullable: false, precision: 7),
                        Log = c.String(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Answers", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Games", new[] { "Location_Id" });
            DropIndex("dbo.Answers", new[] { "Location_Id" });
            DropTable("dbo.Locations");
            DropTable("dbo.Games");
            DropTable("dbo.Answers");
        }
    }
}
