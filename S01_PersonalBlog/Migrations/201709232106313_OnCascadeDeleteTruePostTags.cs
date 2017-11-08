namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnCascadeDeleteTruePostTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts");
            AddForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts", "PostID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts");
            AddForeignKey("dbo.Tags", "Post_PostID", "dbo.Posts", "PostID");
        }
    }
}
