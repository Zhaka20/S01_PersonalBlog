namespace S01_PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostVoteAndCommentVoteModelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostVotes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PostID = c.Int(nullable: false),
                    Value = c.Boolean(),
                    VoterId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.VoterId)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.VoterId);

            CreateTable(
                "dbo.CommentVotes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CommentID = c.Int(nullable: false),
                    Value = c.Boolean(),
                    VoterId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.VoterId)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .Index(t => t.CommentID)
                .Index(t => t.VoterId);

            //RenameTable(name: "dbo.Votes", newName: "CommentVotes");
            //DropForeignKey("dbo.Votes", "CommentID", "dbo.Comments");
            //DropForeignKey("dbo.Votes", "PostID", "dbo.Posts");
            //DropIndex("dbo.CommentVotes", new[] { "PostID" });
            //DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            //DropPrimaryKey("dbo.CommentVotes");


            //AddColumn("dbo.CommentVotes", "Id", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.CommentVotes", "Id");
            //CreateIndex("dbo.CommentVotes", "CommentID");
            //AddForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments", "CommentID", cascadeDelete: true);
            //DropColumn("dbo.CommentVotes", "VoteId");
            //DropColumn("dbo.CommentVotes", "PostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentVotes", "PostID", c => c.Int());
            AddColumn("dbo.CommentVotes", "VoteId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PostVotes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.CommentVotes", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.PostVotes", "VoterId", "dbo.Users");
            DropIndex("dbo.PostVotes", new[] { "VoterId" });
            DropIndex("dbo.PostVotes", new[] { "PostID" });
            DropIndex("dbo.CommentVotes", new[] { "CommentID" });
            DropPrimaryKey("dbo.CommentVotes");
            AlterColumn("dbo.CommentVotes", "CommentID", c => c.Int());
            DropColumn("dbo.CommentVotes", "Id");
            DropTable("dbo.PostVotes");
            AddPrimaryKey("dbo.CommentVotes", "VoteId");
            CreateIndex("dbo.CommentVotes", "CommentID");
            CreateIndex("dbo.CommentVotes", "PostID");
            AddForeignKey("dbo.Votes", "PostID", "dbo.Posts", "PostID");
            AddForeignKey("dbo.Votes", "CommentID", "dbo.Comments", "CommentID");
            RenameTable(name: "dbo.CommentVotes", newName: "Votes");
        }
    }
}
