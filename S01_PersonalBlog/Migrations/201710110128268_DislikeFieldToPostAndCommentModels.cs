namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DislikeFieldToPostAndCommentModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Dislikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Dislikes");
            DropColumn("dbo.Comments", "Dislikes");
        }
    }
}
