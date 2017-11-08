namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsCommenNestingLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "NestingLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "NestingLevel");
        }
    }
}
