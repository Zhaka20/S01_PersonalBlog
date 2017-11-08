namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String(nullable: false));
        }
    }
}
