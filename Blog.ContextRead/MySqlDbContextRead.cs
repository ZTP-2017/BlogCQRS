using Blog.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace Blog.ContextRead
{
    public class MySqlDbContextRead : DbContext
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_read;uid=root;pwd=password;";
        
        public DbSet<ArticleRecord> ArticleDetails { get; set; }
        public DbSet<SectionRecord> SectionDetails { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            
            optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleRecordConfiguration());
            modelBuilder.ApplyConfiguration(new SectionRecordConfiguration());
        }
    }
}