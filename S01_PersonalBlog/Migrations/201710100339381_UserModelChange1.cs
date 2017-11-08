namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChange1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "FistName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FistName", c => c.String());
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
