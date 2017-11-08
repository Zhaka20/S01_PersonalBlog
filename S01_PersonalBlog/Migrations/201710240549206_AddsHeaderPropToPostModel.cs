namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsHeaderPropToPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Header", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Header");
        }
    }
}
