using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WriteSide.Model.ReadSide
{
    public class SectionDetailsRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArticlesCount { get; set; }
    }
    
    public class SectionDetailsRecordConfiguration : IEntityTypeConfiguration<SectionDetailsRecord>
    {
        public void Configure(EntityTypeBuilder<SectionDetailsRecord> builder)
        {
            builder.ToTable("SectionDetails");
        }
    }
}