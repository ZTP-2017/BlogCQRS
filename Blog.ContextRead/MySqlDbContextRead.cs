using Blog.ContextRead.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.ContextRead
{
    public class MySqlDbContextRead : DbContext
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_read;uid=root;pwd=password;";
        
        public DbSet<ArticleRecordRead> ArticleDetails { get; set; }
        public DbSet<SectionRecordRead> SectionDetails { get; set; }
        
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