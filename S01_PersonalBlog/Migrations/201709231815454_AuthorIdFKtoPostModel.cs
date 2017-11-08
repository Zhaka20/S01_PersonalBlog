namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorIdFKtoPostModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorID");
            RenameIndex(table: "dbo.Posts", name: "IX_Author_Id", newName: "IX_AuthorID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_AuthorID", newName: "IX_Author_Id");
            RenameColumn(table: "dbo.Posts", name: "AuthorID", newName: "Author_Id");
        }
    }
}
