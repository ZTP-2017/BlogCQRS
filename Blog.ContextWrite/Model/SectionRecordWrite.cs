using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Models
{
    public class SectionRecordWrite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ArticleRecordWrite> Articles { get; set; }
    }
    
    public class SectionRecordConfiguration : IEntityTypeConfiguration<SectionRecordWrite>
    {
        public void Configure(EntityTypeBuilder<SectionRecordWrite> builder)
        {
            builder.ToTable("Section");
        }
    }
}