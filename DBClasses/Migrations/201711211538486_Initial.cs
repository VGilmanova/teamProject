namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        ToLocation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.ToLocation_Id)
                .Index(t => t.ToLocation_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        WorkTime = c.Time(nullable: false, precision: 7),
                        Log = c.String(),
                        Locaton_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Locaton_Id)
                .Index(t => t.Locaton_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Locaton_Id", "dbo.Locations");
            DropForeignKey("dbo.Answers", "ToLocation_Id", "dbo.Locations");
            DropIndex("dbo.Games", new[] { "Locaton_Id" });
            DropIndex("dbo.Answers", new[] { "ToLocation_Id" });
            DropTable("dbo.Games");
            DropTable("dbo.Locations");
            DropTable("dbo.Answers");
        }
    }
}
