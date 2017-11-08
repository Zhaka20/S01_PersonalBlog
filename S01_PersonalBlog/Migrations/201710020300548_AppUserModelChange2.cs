namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserModelChange2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary());
            AddColumn("dbo.Users", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "NickName");
            DropColumn("dbo.Users", "Image");
        }
    }
}
