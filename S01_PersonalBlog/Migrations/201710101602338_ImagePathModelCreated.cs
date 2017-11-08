namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePathModelCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagePaths",
                c => new
                    {
                        PersonID = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Users", t => t.PersonID)
                .Index(t => t.PersonID);
            
            DropColumn("dbo.Users", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary(storeType: "image"));
            DropForeignKey("dbo.ImagePaths", "PersonID", "dbo.Users");
            DropIndex("dbo.ImagePaths", new[] { "PersonID" });
            DropTable("dbo.ImagePaths");
        }
    }
}
