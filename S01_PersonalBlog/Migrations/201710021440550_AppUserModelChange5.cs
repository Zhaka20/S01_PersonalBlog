namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserModelChange5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "BlogCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BlogCount", c => c.Int(nullable: false));
        }
    }
}
