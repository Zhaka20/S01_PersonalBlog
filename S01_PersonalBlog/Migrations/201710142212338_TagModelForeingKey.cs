namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagModelForeingKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tags", name: "Post_PostID", newName: "PostId");
            RenameIndex(table: "dbo.Tags", name: "IX_Post_PostID", newName: "IX_PostId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tags", name: "IX_PostId", newName: "IX_Post_PostID");
            RenameColumn(table: "dbo.Tags", name: "PostId", newName: "Post_PostID");
        }
    }
}
