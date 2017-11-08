namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "ParentCommentID");
            RenameColumn(table: "dbo.Comments", name: "Comment_CommentID", newName: "ParentCommentID");
            RenameIndex(table: "dbo.Comments", name: "IX_Comment_CommentID", newName: "IX_ParentCommentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_ParentCommentID", newName: "IX_Comment_CommentID");
            RenameColumn(table: "dbo.Comments", name: "ParentCommentID", newName: "Comment_CommentID");
            AddColumn("dbo.Comments", "ParentCommentID", c => c.Int());
        }
    }
}
