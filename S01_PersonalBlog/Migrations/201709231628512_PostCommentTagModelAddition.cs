namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostCommentTagModelAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        Comment_CommentID = c.Int(),
                        Post_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentID)
                .ForeignKey("dbo.Posts", t => t.Post_PostID)
                .Index(t => t.Author_Id)
                .Index(t => t.Comment_CommentID)
                .Index(t => t.Post_PostID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Tile = c.String(),
                        Content = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        Post_PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.Post_PostID })
                .ForeignKey("dbo.Tags", t => t.Tag_TagID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostID, cascadeDelete: true)
                .Index(t => t.Tag_TagID)
                .Index(t => t.Post_PostID);
            
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FistName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPosts", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_TagID", "dbo.Tags");
            DropForeignKey("dbo.Comments", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Comment_CommentID", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropIndex("dbo.TagPosts", new[] { "Post_PostID" });
            DropIndex("dbo.TagPosts", new[] { "Tag_TagID" });
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Post_PostID" });
            DropIndex("dbo.Comments", new[] { "Comment_CommentID" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropColumn("dbo.Users", "FistName");
            DropColumn("dbo.Users", "LastName");
            DropTable("dbo.TagPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
