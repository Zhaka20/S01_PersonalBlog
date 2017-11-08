using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using S01_PersonalBlog.Models;

namespace S01_PersonalBlog.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PersonalBlogDBConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users","dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");

            modelBuilder.Entity<Post>()
                .HasMany<Tag>(c => c.Tags)
                .WithOptional(x => x.Post)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Post>()
               .HasMany<Comment>(c => c.Comments)
               .WithRequired(x => x.Post)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Post>()
               .HasMany<PostVote>(c => c.Votes)
               .WithRequired(x => x.Post)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
               .HasMany<CommentVote>(c => c.Votes)
               .WithRequired(x => x.Comment)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
               .HasMany<CommentVote>(c => c.Votes)
               .WithRequired(x => x.Comment)
               .WillCascadeOnDelete(true);
        }

        public System.Data.Entity.DbSet<S01_PersonalBlog.Models.Post> Posts { get; set; }
        public System.Data.Entity.DbSet<S01_PersonalBlog.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<S01_PersonalBlog.Models.ImagePath> ImagePaths { get; set; }
    }
}