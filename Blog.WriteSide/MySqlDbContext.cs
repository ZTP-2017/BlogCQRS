using Blog.WriteSide.Model.ReadSide;
using Blog.WriteSide.Model.WriteSide;
using Microsoft.EntityFrameworkCore;

namespace Blog.WriteSide
{
    public class MySqlDbContext : DbContext
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_cqrs;uid=root;pwd=password;";
        
        public DbSet<ArticleDetailsRecord> ArticleDetails { get; set; }
        public DbSet<SectionDetailsRecord> SectionDetails { get; set; }
        
        public DbSet<ArticleRecord> Articles { get; set; }
        public DbSet<SectionRecord> Sections { get; set; }
        
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
            modelBuilder.ApplyConfiguration(new ArticleDetailsRecordConfiguration());
            modelBuilder.ApplyConfiguration(new SectionDetailsRecordConfiguration());
            
            modelBuilder.ApplyConfiguration(new ArticleRecordConfiguration());
            modelBuilder.ApplyConfiguration(new SectionRecordConfiguration());
        }
    }
}