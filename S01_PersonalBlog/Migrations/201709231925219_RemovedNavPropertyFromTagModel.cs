namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNavPropertyFromTagModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPosts", "Tag_TagID", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "Post_PostID", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Tag_TagID" });
            DropIndex("dbo.TagPosts", new[] { "Post_PostID" });
            AddColumn("dbo.Tags", "Post_PostID", c => c.Int());
            CreateIndex("dbo.Tags", "Post_PostID");
            AddForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts", "PostID");
            DropTable("dbo.TagPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        Post_PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.Post_PostID });
            
            DropForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts");
            DropIndex("dbo.Tags", new[] { "Post_PostID" });
            DropColumn("dbo.Tags", "Post_PostID");
            CreateIndex("dbo.TagPosts", "Post_PostID");
            CreateIndex("dbo.TagPosts", "Tag_TagID");
            AddForeignKey("dbo.TagPosts", "Post_PostID", "dbo.Posts", "PostID", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Tag_TagID", "dbo.Tags", "TagID", cascadeDelete: true);
        }
    }
}
