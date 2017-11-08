namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableForeingKeyOnVote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.PostVotes", "PostID", "dbo.Posts");
            DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            DropIndex("dbo.PostVotes", new[] { "PostID" });
            AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int());
            AlterColumn("dbo.PostVotes", "PostID", c => c.Int());
            CreateIndex("dbo.CommentVotes", "CommentID");
            CreateIndex("dbo.PostVotes", "PostID");
            AddForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments", "CommentID");
            AddForeignKey("dbo.PostVotes", "PostID", "dbo.Posts", "PostID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostVotes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments");
            DropIndex("dbo.PostVotes", new[] { "PostID" });
            DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            AlterColumn("dbo.PostVotes", "PostID", c => c.Int(nullable: false));
            AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int(nullable: false));
            CreateIndex("dbo.PostVotes", "PostID");
            CreateIndex("dbo.CommentVotes", "CommentID");
            AddForeignKey("dbo.PostVotes", "PostID", "dbo.Posts", "PostID", cascadeDelete: true);
            AddForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments", "CommentID", cascadeDelete: true);
        }
    }
}
