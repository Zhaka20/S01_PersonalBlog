namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TileToTitleReplace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Title", c => c.String());
            DropColumn("dbo.Posts", "Tile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tile", c => c.String());
            DropColumn("dbo.Posts", "Title");
        }
    }
}
