using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.User)
                .HasForeignKey<Blog>(x => x.UserId);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.BlogPosts)
                .WithOne(b => b.Blog)
                .HasForeignKey(k => k.BlogId);
                
            modelBuilder.Entity<BlogPost>()
                .HasOne(b => b.Content)
                .WithOne(c => c.BlogPost)
                .HasForeignKey<Content>(x => x.BlogPostId);

            modelBuilder.Entity<Content>()
                .HasMany(c => c.Sections)
                .WithOne(s => s.Content)
                .HasForeignKey(x => x.ContentId);

            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.BlogPost)
                .HasForeignKey(x => x.BlogPostId);

        }
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Content> Contents => Set<Content>();
        public DbSet<Section> Sections => Set<Section>();
    }
}