using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Models
 {
     public class ArticleRecordWrite
     {
         public int Id { get; set; }
         public string Title { get; set; }
         public DateTime Date { get; set; }
         public string Text { get; set; }
         public string ImageUrl { get; set; }
         public int SectionId { get; set; }
         public SectionRecordWrite Section { get; set; }
     }
     
     public class ArticleRecordConfiguration : IEntityTypeConfiguration<ArticleRecordWrite>
     {
         public void Configure(EntityTypeBuilder<ArticleRecordWrite> builder)
         {
             builder.ToTable("Article");
         }
     }
 }