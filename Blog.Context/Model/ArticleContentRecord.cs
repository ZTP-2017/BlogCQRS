using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Context.Model
{
    public class ArticleContentRecord
    {
        public int Id { get; set; }
        public ArticleRecord Article { get; set; }
        
        public string Text { get; set; }
        public string Image { get; set; }
    }
    
    public class ArticleContentRecordConfiguration : IEntityTypeConfiguration<ArticleContentRecord>
    {
        public void Configure(EntityTypeBuilder<ArticleContentRecord> builder)
        {
            builder
                .HasOne(e => e.Article).WithOne(e => e.Content)
                .HasForeignKey<ArticleRecord>(e => e.Id);
            builder.ToTable("Articles");
        }
    }
}