namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKPostIDtoCommentModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "Post_PostID", newName: "PostID");
            RenameIndex(table: "dbo.Comments", name: "IX_Post_PostID", newName: "IX_PostID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_PostID", newName: "IX_Post_PostID");
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_PostID");
        }
    }
}
