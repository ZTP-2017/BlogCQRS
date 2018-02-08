using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WriteSide.Models.Write
{
    public class SectionRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ArticleRecord> Articles { get; set; }
    }
    
    public class SectionRecordConfiguration : IEntityTypeConfiguration<SectionRecord>
    {
        public void Configure(EntityTypeBuilder<SectionRecord> builder)
        {
            builder.ToTable("Section");
        }
    }
}