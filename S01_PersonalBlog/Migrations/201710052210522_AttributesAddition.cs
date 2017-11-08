namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributesAddition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Users", "NickName", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Tags", "Title", c => c.String(nullable: false, maxLength: 60));
            DropColumn("dbo.Comments", "Dislikes");
            DropColumn("dbo.Posts", "Dislikes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Dislikes", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "Dislikes", c => c.Int(nullable: false));
            AlterColumn("dbo.Tags", "Title", c => c.String());
            AlterColumn("dbo.Posts", "Content", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Users", "NickName", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
        }
    }
}
