using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.ContextWrite
{
    public class MySqlDbContextWrite : DbContext
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_write;uid=root;pwd=password;";
        
        public DbSet<ArticleRecordWrite> Articles { get; set; }
        public DbSet<SectionRecordWrite> Sections { get; set; }
        
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