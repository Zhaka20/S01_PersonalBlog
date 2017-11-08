namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotingModelAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Value = c.Boolean(),
                        VoterId = c.String(maxLength: 128),
                        Post_PostID = c.Int(),
                        Comment_CommentID = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Users", t => t.VoterId)
                .ForeignKey("dbo.Posts", t => t.Post_PostID)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentID)
                .Index(t => t.VoterId)
                .Index(t => t.Post_PostID)
                .Index(t => t.Comment_CommentID);
            
            DropColumn("dbo.Comments", "Likes");
            DropColumn("dbo.Comments", "Dislikes");
            DropColumn("dbo.Posts", "Likes");
            DropColumn("dbo.Posts", "Dislikes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "Likes", c => c.Int(nullable: false));
            DropForeignKey("dbo.Votes", "Comment_CommentID", "dbo.Comments");
            DropForeignKey("dbo.Votes", "Post_PostID", "dbo.Posts");
            DropForeignKey("dbo.Votes", "VoterId", "dbo.Users");
            DropIndex("dbo.Votes", new[] { "Comment_CommentID" });
            DropIndex("dbo.Votes", new[] { "Post_PostID" });
            DropIndex("dbo.Votes", new[] { "VoterId" });
            DropTable("dbo.Votes");
        }
    }
}
