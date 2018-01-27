using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WriteSide.Model.ReadSide
{
    public class ArticleDetailsRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int SectionId { get; set; }
    }

    public class ArticleDetailsRecordConfiguration : IEntityTypeConfiguration<ArticleDetailsRecord>
    {
        public void Configure(EntityTypeBuilder<ArticleDetailsRecord> builder)
        {
            builder.ToTable("ArticleDetails");
        }
    }
}