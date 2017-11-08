namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserModelChange4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BlogCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "BlogCount");
        }
    }
}
