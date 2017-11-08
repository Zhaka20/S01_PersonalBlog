namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsNestedCommentPropetyToCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsNestedComment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IsNestedComment");
        }
    }
}
