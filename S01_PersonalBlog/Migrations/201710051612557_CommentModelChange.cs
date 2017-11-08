namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ParentCommentID", c => c.Int());
            DropColumn("dbo.Comments", "IsNestedComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "IsNestedComment", c => c.Boolean(nullable: false));
            DropColumn("dbo.Comments", "ParentCommentID");
        }
    }
}
