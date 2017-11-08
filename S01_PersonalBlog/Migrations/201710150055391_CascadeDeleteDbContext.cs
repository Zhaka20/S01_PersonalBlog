namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteDbContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.PostVotes", "PostID", "dbo.Posts");
            DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            DropIndex("dbo.PostVotes", new[] { "PostID" });
            AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int(nullable: false));
            AlterColumn("dbo.PostVotes", "PostID", c => c.Int(nullable: false));
            CreateIndex("dbo.CommentVotes", "CommentID");
            CreateIndex("dbo.PostVotes", "PostID");
            AddForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments", "CommentID", cascadeDelete: true);
            AddForeignKey("dbo.PostVotes", "PostID", "dbo.Posts", "PostID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostVotes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments");
            DropIndex("dbo.PostVotes", new[] { "PostID" });
            DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            AlterColumn("dbo.PostVotes", "PostID", c => c.Int());
            AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int());
            CreateIndex("dbo.PostVotes", "PostID");
            CreateIndex("dbo.CommentVotes", "CommentID");
            AddForeignKey("dbo.PostVotes", "PostID", "dbo.Posts", "PostID");
            AddForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments", "CommentID");
        }
    }
}
