using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Blogs.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
    
            optionsBuilder.UseSqlite(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
                .HasOne(c => c.Content);

            modelBuilder.Entity<Content>()
                .HasMany(s => s.Sections);
        }

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Content> Contents => Set<Content>();
        public DbSet<Section> Sections => Set<Section>();
    }
}