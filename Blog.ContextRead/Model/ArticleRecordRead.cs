using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.ContextRead.Models
 {
     public class ArticleRecordRead
     {
         public int Id { get; set; }
         public string Title { get; set; }
         public DateTime Date { get; set; }
         public string Text { get; set; }
         public string ImageUrl { get; set; }
         public int SectionId { get; set; }
         public SectionRecordRead Section { get; set; }
     }
     
     public class ArticleRecordConfiguration : IEntityTypeConfiguration<ArticleRecordRead>
     {
         public void Configure(EntityTypeBuilder<ArticleRecordRead> builder)
         {
             builder.ToTable("Article");
         }
     }
 }