using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.WriteSide.Model.WriteSide
 {
     public class ArticleRecord
     {
         public int Id { get; set; }
         public string Title { get; set; }
         public DateTime Date { get; set; }
         public string Text { get; set; }
         public string ImageUrl { get; set; }
         public int SectionId { get; set; }
         public SectionRecord Section { get; set; }
     }
     
     public class ArticleRecordConfiguration : IEntityTypeConfiguration<ArticleRecord>
     {
         public void Configure(EntityTypeBuilder<ArticleRecord> builder)
         {
             builder.ToTable("Article");
         }
     }
 }