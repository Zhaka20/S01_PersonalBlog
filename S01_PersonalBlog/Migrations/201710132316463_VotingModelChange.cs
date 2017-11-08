namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotingModelChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Votes", name: "Comment_CommentID", newName: "CommentID");
            RenameColumn(table: "dbo.Votes", name: "Post_PostID", newName: "PostID");
            RenameIndex(table: "dbo.Votes", name: "IX_Post_PostID", newName: "IX_PostID");
            RenameIndex(table: "dbo.Votes", name: "IX_Comment_CommentID", newName: "IX_CommentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Votes", name: "IX_CommentID", newName: "IX_Comment_CommentID");
            RenameIndex(table: "dbo.Votes", name: "IX_PostID", newName: "IX_Post_PostID");
            RenameColumn(table: "dbo.Votes", name: "PostID", newName: "Post_PostID");
            RenameColumn(table: "dbo.Votes", name: "CommentID", newName: "Comment_CommentID");
        }
    }
}
