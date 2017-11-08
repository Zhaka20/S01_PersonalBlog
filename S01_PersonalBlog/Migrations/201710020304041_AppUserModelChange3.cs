namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserModelChange3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Image", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Image", c => c.Binary());
        }
    }
}
