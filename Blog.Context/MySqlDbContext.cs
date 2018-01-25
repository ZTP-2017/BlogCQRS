using Blog.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Context
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<ArticleRecord> Articles { get; set; }

        public MySqlDbContext()
        {
            
        }
        
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options): base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            
            optionsBuilder.UseMySql(@"Server=localhost;database=blog;uid=root;pwd=password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleRecordConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleContentRecordConfiguration());
        }
    }
}