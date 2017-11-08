namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentCascadeDelete3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ParentCommentID" });
            AlterColumn("dbo.Comments", "ParentCommentID", c => c.Int());
            CreateIndex("dbo.Comments", "ParentCommentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ParentCommentID" });
            AlterColumn("dbo.Comments", "ParentCommentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ParentCommentID");
        }
    }
}
