namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBloggerIdToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BloggerId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "BloggerId");
        }
    }
}
