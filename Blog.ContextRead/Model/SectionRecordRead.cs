using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.ContextRead.Models
{
    public class SectionRecordRead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ArticleRecordRead> Articles { get; set; }
    }
    
    public class SectionRecordConfiguration : IEntityTypeConfiguration<SectionRecordRead>
    {
        public void Configure(EntityTypeBuilder<SectionRecordRead> builder)
        {
            builder.ToTable("Section");
        }
    }
}